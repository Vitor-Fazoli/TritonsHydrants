using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TritonsHydrants.Content.NPCs
{
    /// <summary>
    /// 
    /// </summary>
    public class Victrimeire : ModNPC
    {
        private static readonly Profiles.StackedNPCProfile NPCProfile;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.DangerDetectRange[Type] = 0;
            NPCID.Sets.PrettySafe[Type] = 300;
            NPCID.Sets.ShimmerTownTransform[NPC.type] = false;

            NPCID.Sets.ActsLikeTownNPC[Type] = true;

            NPCID.Sets.NoTownNPCHappiness[Type] = true;

            NPCID.Sets.SpawnsWithCustomName[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 0f, 
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 75;
            NPC.height = 90;
            NPC.damage = 1;
            NPC.defense = 15;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
        }

        public override bool CanChat()
        {
            return true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				new FlavorTextBestiaryInfoElement("Mods.TritonsHydrants.Bestiary.Victrimeire"),
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            SoundEngine.PlaySound(SoundID.AbigailCry, Entity.position);

            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BloodWater);
            }
        }

        public override void AI()
        {
            
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override List<string> SetNPCNameList()
        {
            return
            [
                "Victrimeire"
            ];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue3"));
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); // This is the key to the word "Shop"
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = "Shop";
            }
        }

        public override void AddShops()
        {
            new NPCShop(Type)
                .Add(ItemID.SharkFin)
                .Register();
        }
    }
}