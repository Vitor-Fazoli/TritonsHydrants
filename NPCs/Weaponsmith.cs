using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using GearonArsenalMod.Item;
using GearonArsenalMod.Item.Materials;

namespace GearonArsenalMod.NPCs
{
	[AutoloadHead]
	public class Weaponsmith : ModNPC
	{
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 25;

			NPCID.Sets.ExtraFramesCount[Type] = 9;
			NPCID.Sets.AttackFrameCount[Type] = 4;
			NPCID.Sets.DangerDetectRange[Type] = 700;
			NPCID.Sets.AttackType[Type] = 0;
			NPCID.Sets.AttackTime[Type] = 90;
			NPCID.Sets.AttackAverageChance[Type] = 30;
			NPCID.Sets.HatOffsetY[Type] = 4;

			// Influences how the NPC looks in the Bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				Velocity = 0.75f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
				// Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
				// If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
			};

			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
		}

		public override void SetDefaults() {
			NPC.townNPC = true; // Sets NPC to be a Town NPC
			NPC.friendly = true; // NPC Will not attack player
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 20;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;

			AnimationType = NPCID.Guide;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Hailing from a mysterious greyscale cube world, the Example Person is here to help you understand everything about tModLoader."),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("Mods.ExampleMod.Bestiary.ExamplePerson")
			});
		}

		// The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
		// Returning false will allow you to manually draw your NPC
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
			//This code slowly rotates the NPC in the bestiary
			//(simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
			if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers)) {
                drawModifiers.Rotation += 0.001f;

				// Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
				NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
				NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
			}

			return true;
		}

		public override void HitEffect(int hitDirection, double damage) {
			int num = NPC.life > 0 ? 1 : 5;

			for (int k = 0; k < num; k++) {
				Dust.NewDust(NPC.position, NPC.width, NPC.height,DustID.Cloud);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			for (int k = 0; k < 255; k++) {
				Player player = Main.player[k];
				if (!player.active) {
					continue;
				}

				if (player.inventory.Any(item => item.type == ModContent.ItemType<CopperGreatsword>() || item.type == ModContent.ItemType<TinGreatsword>())) {
					return true;
				}
			}

			return false;
		}

		// Example Person needs a house built out of ExampleMod tiles. You can delete this whole method in your townNPC for the regular house conditions.
		public override bool CheckConditions(int left, int right, int top, int bottom) {
			int score = 0;
			for (int x = left; x <= right; x++) {
				for (int y = top; y <= bottom; y++) {
					int type = Main.tile[x, y].type;
					
					if (type == TileID.Furnaces && type == TileID.Torches) {
						score++;
					}

					if (Main.tile[x, y].wall == WallID.Stone) {
						score++;
					}
				}
			}

			return score >= ((right - left) * (bottom - top)) / 2;
		}
		public override void FindFrame(int frameHeight) {
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat() {
			WeightedRandom<string> chat = new WeightedRandom<string>();

			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4)) {
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}

            if (!Main.LocalPlayer.HasItem(ModContent.ItemType<BrokenHammer>())){
				chat.Add("Could you take my hammer back from that Cursed Eye", 5.0);
			}
            else
            {
				chat.Add("Sometimes I feel like I'm different from everyone else here.");
				chat.Add("What's your favorite color? My favorite colors are white and black.");
				chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
				chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
				chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			}

			return chat;
		}
		public override string TownNPCName()
		{
			return "Gearon";
		}

		public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Awesomeify";

			if (Main.LocalPlayer.HasItem(ModContent.ItemType<BrokenHammer>())) {
				button = "Give " + Lang.GetItemNameValue(ModContent.ItemType<BrokenHammer>());
			}
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {

				if (Main.LocalPlayer.HasItem(ModContent.ItemType<BrokenHammer>())) {
					SoundEngine.PlaySound(SoundID.Item37);

					Main.npcChatText = $"I fixed it, now you can use it to craft better Greatswords";

					int HammerItemIndex = Main.LocalPlayer.FindItem(ModContent.ItemType<BrokenHammer>());

					Main.LocalPlayer.inventory[HammerItemIndex].TurnToAir();
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<PatchedHammer>());

					return;
				}

				shop = true;
			}
		}

        // Not completely finished, but below is what the NPC will sell

        //public override void SetupShop(Chest shop, ref int nextSlot)
        //{
        //    shop.item[nextSlot++].SetDefaults(ItemType<ExampleItem>());
        //    shop.item[nextSlot].SetDefaults(ItemType<EquipMaterial>());
        //    nextSlot++;
        //    shop.item[nextSlot].SetDefaults(ItemType<BossItem>());
        //    nextSlot++;
        //    shop.item[nextSlot++].SetDefaults(ItemType<Items.Placeable.Furniture.ExampleWorkbench>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<Items.Placeable.Furniture.ExampleChair>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<Items.Placeable.Furniture.ExampleDoor>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<Items.Placeable.Furniture.ExampleBed>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<Items.Placeable.Furniture.ExampleChest>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<ExamplePickaxe>());
        //    shop.item[nextSlot++].SetDefaults(ItemType<ExampleHamaxe>());

        //    if (Main.LocalPlayer.HasBuff(BuffID.Lifeforce))
        //    {
        //        shop.item[nextSlot++].SetDefaults(ItemType<ExampleHealingPotion>());
        //    }

        //    if (Main.LocalPlayer.GetModPlayer<ExamplePlayer>().ZoneExample && !GetInstance<ExampleConfigServer>().DisableExampleWings)
        //    {
        //        shop.item[nextSlot].SetDefaults(ItemType<ExampleWings>());
        //        nextSlot++;
        //    }

        //    if (Main.moonPhase < 2)
        //    {
        //        shop.item[nextSlot++].SetDefaults(ItemType<ExampleSword>());
        //    }
        //    else if (Main.moonPhase < 4)
        //    {
        //        shop.item[nextSlot++].SetDefaults(ItemType<ExampleGun>());
        //        shop.item[nextSlot].SetDefaults(ItemType<ExampleBullet>());
        //    }
        //    else if (Main.moonPhase < 6)
        //    {
        //        shop.item[nextSlot++].SetDefaults(ItemType<ExampleStaff>());
        //    }

        //todo: Here is an example of how your npc can sell items from other mods.
        //     var modSummonersAssociation = ModLoader.GetMod("SummonersAssociation");
        //    if (modSummonersAssociation != null)
        //    {
        //        shop.item[nextSlot].SetDefaults(modSummonersAssociation.ItemType("BloodTalisman"));
        //        nextSlot++;
        //    }

        //    if (!Main.LocalPlayer.GetModPlayer<ExamplePlayer>().examplePersonGiftReceived && GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList != null)
        //    {
        //        foreach (var item in GetInstance<ExampleConfigServer>().ExamplePersonFreeGiftList)
        //        {
        //            if (Item.IsUnloaded) continue;
        //            shop.item[nextSlot].SetDefaults(Item.Type);
        //            shop.item[nextSlot].shopCustomPrice = 0;
        //            shop.item[nextSlot].GetGlobalItem<ExampleInstancedGlobalItem>().examplePersonFreeGift = true;
        //            nextSlot++;
        //            // TODO: Have tModLoader handle index issues.
        //        }
        //    }
        //}
        public override void ModifyNPCLoot(NPCLoot npcLoot) {
			 npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CopperGreatsword>(), 1));
		}

		public override bool CanGoToStatue(bool toKingStatue) => true;

		public override void OnGoToStatue(bool toKingStatue) {
			if (Main.netMode == NetmodeID.Server) {
				ModPacket packet = Mod.GetPacket();
				packet.Write((byte)NPC.whoAmI);
				packet.Send();
			}
			else {
				StatueTeleport();
			}
		}
		public void StatueTeleport() {
			for (int i = 0; i < 30; i++) {
				Vector2 position = Main.rand.NextVector2Square(-20, 21);
				if (Math.Abs(position.X) > Math.Abs(position.Y)) {
					position.X = Math.Sign(position.X) * 20;
				}
				else {
					position.Y = Math.Sign(position.Y) * 20;
				}

				Dust.NewDustPerfect(NPC.Center + position,DustID.Cloud, Vector2.Zero).noGravity = true;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 30;
			randExtraCooldown = 30;
		}
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}