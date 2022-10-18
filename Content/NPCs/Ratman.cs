using DevilsWarehouse.Common.Abstract;
using DevilsWarehouse.Content.Items.Artifacts;
using DevilsWarehouse.Content.Items.Weapons.Melee.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace DevilsWarehouse.Content.NPCs
{
    [AutoloadHead]
    public class Ratman : ModNPC
    {
        public int NumberOfTimesTalkedTo = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ratman");
            Main.npcFrameCount[Type] = 10;


            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = -1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Love) // Example Person likes the Example Surface Biome
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.Painter, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Hate) // Hates living near the demolitionist.
            ; // < Mind the semicolon!
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 48;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

                new FlavorTextBestiaryInfoElement("came from deep lands, he became a thinking being after touching where he shouldn't have"),
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                if (player.inventory.Any(item => item.rare == ModContent.RarityType<ArtifactRarity>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            int score = 0;
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    int type = Main.tile[x, y].TileType;
                    if (type == TileID.Ebonstone || type == TileID.Chairs || type == TileID.TinkerersWorkbench)
                    {
                        score++;
                    }

                    if (Main.tile[x, y].WallType == WallID.EbonstoneBrick)
                    {
                        score++;
                    }
                }
            }

            return score >= ((right - left) * (bottom - top)) / 2;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new RatmanProfile();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Schirit"
            };
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 1;
            int finalFrame = 6;
            int frameSpeed = 5;

            NPC.spriteDirection = NPC.direction;


            if (NPC.AnyInteractions())
            {
                NPC.frame.Y = 7 * frameHeight;

                NPC.frameCounter += 0.5f;
                NPC.frameCounter += NPC.velocity.Length() / 10f;
                if (NPC.frameCounter > frameSpeed)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;

                    if (NPC.frame.Y > 9 * frameHeight)
                    {
                        NPC.frame.Y = 9 * frameHeight;
                    }
                }
            }
            else if (NPC.velocity.X != 0)
            {
                NPC.frameCounter += 0.5f;
                NPC.frameCounter += NPC.velocity.Length() / 10f;
                if (NPC.frameCounter > frameSpeed)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;

                    if (NPC.frame.Y > finalFrame * frameHeight)
                    {
                        NPC.frame.Y = startFrame * frameHeight;
                    }
                }
            }
            else
            {
                NPC.frame.Y = (startFrame - 1) * frameHeight;
            }
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int bestiaryGirl = NPC.FindFirstNPC(NPCID.BestiaryGirl);
            if (bestiaryGirl >= 0 && Main.rand.NextBool(4))
            {
                //Ela se parece um pouco comigo, Você não acha ?
                chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.PartyGirlDialogue", Main.npc[bestiaryGirl].GivenName));
            }
            //de onde eu vim sempre era muito escuro, por isso não gosto do sol
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.CommonDialogue"), 5.0);
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.RareDialogue"), 0.1);

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
                chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.TalkALot"));
            }

            return chat; // chat is implicitly cast to a string.
        }

        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Awesomeify";
            if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
            {
                button = "Upgrade " + Lang.GetItemNameValue(ItemID.HiveBackpack);
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                // We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.

                if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
                {
                    SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                    Main.npcChatText = $"I upgraded your {Lang.GetItemNameValue(ItemID.HiveBackpack)} to a {Lang.GetItemNameValue(ModContent.ItemType<Maiden>())}";

                    int hiveBackpackItemIndex = Main.LocalPlayer.FindItem(ItemID.HiveBackpack);
                    var entitySource = NPC.GetSource_GiftOrReward();

                    Main.LocalPlayer.inventory[hiveBackpackItemIndex].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(entitySource, ModContent.ItemType<Maiden>());

                    return;
                }

                shop = true;
            }
        }

        // Not completely finished, but below is what the NPC will sell

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<SteroidSyringe>());
            // shop.item[nextSlot].SetDefaults(ItemType<EquipMaterial>());
            // nextSlot++;
            // shop.item[nextSlot].SetDefaults(ItemType<BossItem>());
            // nextSlot++;

            if (Main.LocalPlayer.HasBuff(BuffID.Lifeforce))
            {
                shop.item[nextSlot++].SetDefaults(ItemID.BattlePotion);
            }

            // if (Main.LocalPlayer.GetModPlayer<ExamplePlayer>().ZoneExample && !GetInstance<ExampleConfigServer>().DisableExampleWings) {
            // 	shop.item[nextSlot].SetDefaults(ItemType<ExampleWings>());
            // 	nextSlot++;
            // }

            if (Main.moonPhase < 2)
            {
                shop.item[nextSlot++].SetDefaults(ItemID.DungeonDesertKey);
            }
            else if (Main.moonPhase < 4)
            {
                // shop.item[nextSlot++].SetDefaults(ItemType<ExampleGun>());
                shop.item[nextSlot].SetDefaults(ItemID.IllegalGunParts);
            }
            else if (Main.moonPhase < 6)
            {
                // shop.item[nextSlot++].SetDefaults(ItemType<ExampleStaff>());
            }

            // if (!Main.LocalPlayer.GetModPlayer<ExamplePlayer>().examplePersonGiftReceived && GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList != null) {
            // 	foreach (var item in GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList) {
            // 		if (Item.IsUnloaded) continue;
            // 		shop.item[nextSlot].SetDefaults(Item.Type);
            // 		shop.item[nextSlot].shopCustomPrice = 0;
            // 		shop.item[nextSlot].GetGlobalItem<ExampleInstancedGlobalItem>().examplePersonFreeGift = true;
            // 		nextSlot++;
            // 		//TODO: Have tModLoader handle index issues.
            // 	}
            // }
        }

        public override void ModifyNPCLoot(NPCLoot NPCLoot)
        {

        }
        public override bool CanGoToStatue(bool toKingStatue) => true;
        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }
        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }

                Dust.NewDustPerfect(NPC.Center + position, DustID.MinecartSpark, Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        // todo: implement
        // public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
        // 	projType = ProjectileType<SparklingBall>();
        // 	attackDelay = 1;
        // }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        public override void LoadData(TagCompound tag)
        {
            NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        }
    }

    public class RatmanProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC NPC) => NPC.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC NPC)
        {
            if (NPC.IsABestiaryIconDummy && !NPC.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("DevilsWarehouse/Content/NPCs/Ratman");

            return ModContent.Request<Texture2D>("DevilsWarehouse/Content/NPCs/Ratman");
        }

        public int GetHeadTextureIndex(NPC NPC) => ModContent.GetModHeadSlot("DevilsWarehouse/Content/NPCs/Ratman_Head");
    }
}