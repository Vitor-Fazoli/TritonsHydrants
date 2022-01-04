using GearonArsenalMod.Content.Item.Accessories;
using GearonArsenalMod.Content.Item.Weapons;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.NPCs.TownNPCs {
    [AutoloadHead]
    public class Blacksmith : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;
        }

        public override void SetDefaults() {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 34;
            NPC.height = 56;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {

            for (int k = 0; k < 255; k++) {
                Player player = Main.player[k];

                if (!player.active) {
                    continue;
                }

                if (player.inventory.Any(item => item.type == ModContent.ItemType<Balm>() || item.type == ModContent.ItemType<Lantern>())) {
                    return true;
                }
            }

            return false;
        }
        public override void FindFrame(int frameHeight) {

            NPC.frameCounter++;
            NPC.spriteDirection = NPC.direction;

            int i = 5;

            if (NPC.velocity.X == 0) {
                NPC.frame.Y = 4 * frameHeight;
            } else {
                if (NPC.frameCounter < i * 1) {
                    NPC.frame.Y = 0 * frameHeight;
                } else if (NPC.frameCounter < i * 1) {
                    NPC.frame.Y = 1 * frameHeight;
                } else if (NPC.frameCounter < i * 2) {
                    NPC.frame.Y = 2 * frameHeight;
                } else if (NPC.frameCounter < i * 3) {
                    NPC.frame.Y = 3 * frameHeight;
                } else if (NPC.frameCounter < i * 4) {
                    NPC.frame.Y = 4 * frameHeight;
                } else if (NPC.frameCounter < i * 5) {
                    NPC.frame.Y = 5 * frameHeight;
                } else if (NPC.frameCounter < i * 6) {
                    NPC.frame.Y = 6 * frameHeight;
                } else if (NPC.frameCounter < i * 7) {
                    NPC.frame.Y = 7 * frameHeight;
                } else {
                    NPC.frameCounter = 0;
                }
            }
        }
        public override string TownNPCName() {

            return "Gearon";
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ModContent.ProjectileType<IronClaymoreP>();
            attackDelay = 1;
        }
    }
}
