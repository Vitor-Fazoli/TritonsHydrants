using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.NPCs.Enemies
{
    public class Flyer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 86;
            NPC.lifeMax = 2100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.defense = 10;
            NPC.knockBackResist = 0.1f;
            NPC.netAlways = true;
            NPC.lavaImmune = true;
            NPC.npcSlots = 5f;
            NPC.damage = 40;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 3;
            int frameSpeed = 3;

            NPC.spriteDirection = NPC.direction;

                NPC.frameCounter += 0.5f;
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
    }
}
