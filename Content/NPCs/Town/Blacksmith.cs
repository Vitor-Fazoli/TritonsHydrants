using System.Linq;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace GearonArsenal.Content.NPCs.Town {
    [AutoloadHead]
    public class Blacksmith : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 20;
            NPCID.Sets.ExtraFramesCount[Type] = 13;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;

            NPC.Happiness
            .SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
            .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike)
            .SetBiomeAffection<JungleBiome>(AffectionLevel.Love)
            .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
            .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Like)
            .SetNPCAffection(NPCID.Angler, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Hate)
            ;
        }

        public override void SetDefaults() {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 40;
            NPC.height = 44;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.lifeMax = 250;
            NPC.defense = 10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {

            for (int k = 0; k < 255; k++) {
                Player player = Main.player[k];

                if (!player.active) {
                    continue;
                }

                //if (player.inventory.Any(item => item.type == ModContent.ItemType<Balm>() || item.type == ModContent.ItemType<Lantern>())) {
                //    return true;
                //}
            }

            return false;
        }
        public override string TownNPCName() {

            return "Gearon";
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {

            attackDelay = 1;
        }

        public override string GetChat() {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            if (dryad >= 0 && Main.rand.NextBool(4)) {
                chat.Add(Main.npc[dryad].GivenName + "is the one who understands me the most around here, I love her");
            }

            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(4)) {
                chat.Add(Main.npc[partyGirl].GivenName + "insists on trying to put a hat on me");
            }
            

            chat.Add("Sometimes I feel like I'm different from everyone else here.");
            chat.Add("What's your favorite color? My favorite colors are white and black.");
            chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
            chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
            chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
            return chat;
        }

        public override bool UsesPartyHat() {
            return false;
        }
    }
}