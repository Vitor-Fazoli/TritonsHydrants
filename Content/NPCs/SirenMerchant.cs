using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TritonsHydrants.Content.NPCs
{
    public class SirenMerchant : ModNPC
    {
        private static Profiles.StackedNPCProfile NPCProfile;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.DangerDetectRange[Type] = 0;
            NPCID.Sets.PrettySafe[Type] = 300;
            NPCID.Sets.ShimmerTownTransform[NPC.type] = false;

            // This sets entry is the most important part of this NPC. Since it is true, it tells the game that we want this NPC to act like a town NPC without ACTUALLY being one.
            // What that means is: the NPC will have the AI of a town NPC, will attack like a town NPC, and have a shop (or any other additional functionality if you wish) like a town NPC.
            // However, the NPC will not have their head displayed on the map, will de-spawn when no players are nearby or the world is closed, and will spawn like any other NPC.
            NPCID.Sets.ActsLikeTownNPC[Type] = true;

            // This prevents the happiness button
            NPCID.Sets.NoTownNPCHappiness[Type] = true;

            // To reiterate, since this NPC isn't technically a town NPC, we need to tell the game that we still want this NPC to have a custom/randomized name when they spawn.
            // In order to do this, we simply make this hook return true, which will make the game call the TownNPCName method when spawning the NPC to determine the NPC's name.
            NPCID.Sets.SpawnsWithCustomName[Type] = true;

            // The vanilla Bone Merchant cannot interact with doors (open or close them, specifically), but if you want your NPC to be able to interact with them despite this,
            // uncomment this line below.
            //NPCID.Sets.AllowDoorInteraction[Type] = true;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 0f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // NPCProfile = new Profiles.StackedNPCProfile(
            //     new Profiles.DefaultNPCProfile(Texture, -1),
            //     new Profiles.DefaultNPCProfile(Texture + "_Shimmer", -1)
            // );
        }

        public override void SetDefaults()
        {
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 148;
            NPC.height = 180;
            NPC.aiStyle = 0;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        }

        public override bool CanChat()
        {
            return true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange([
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				// Sets your NPC's flavor text in the bestiary. (use localization keys)
				new FlavorTextBestiaryInfoElement("Mods.TritonsHydrants.Bestiary.ExampleBoneMerchant_1"),
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BloodWater);
            }

            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {

            }
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override List<string> SetNPCNameList()
        {
            return
            [
                "Lurienna"
            ];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue3"));
            return chat; // chat is implicitly cast to a string.
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