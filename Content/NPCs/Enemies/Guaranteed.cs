using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;

namespace GearonArsenal.Content.NPCs.Enemies
{
    public class Guaranteed : ModNPC
    {
        private int jump = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 10;
        }
        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 60;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.defense = 10;
            NPC.knockBackResist = 0.1f;
            NPC.netAlways = true;
            NPC.lavaImmune = true;
            NPC.damage = 40;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 1;
            int finalFrame = 8;
            int frameSpeed = 5;

            NPC.spriteDirection = NPC.direction;


            if (NPC.velocity.Y != 0)
            {
                NPC.frame.Y = (finalFrame + 1) * frameHeight;
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
        public override void AI()
        {
            NPC.TargetClosest(faceTarget: true);
            Player target = Main.player[NPC.target];

            Moviment(target);

            if ((NPC.collideX && NPC.collideY) || NPC.collideX)
            {
                Teleport(target);
            }

            //Uso para testes: CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, 10, 10), CombatText.HealLife, (int)NPC.velocity.X);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int backGoreType = Mod.Find<ModGore>("Guaranteed_Back").Type;
                int bodyGoreType = Mod.Find<ModGore>("Guaranteed_Body").Type;
                int headGoreType = Mod.Find<ModGore>("Guaranteed_Head").Type;

                var entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), backGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), bodyGoreType);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), headGoreType);
            }
        }
        private void Moviment(Player target)
        {

            #region DynamicVelocity
            Vector2 moveTo = target.Center;

            float speed = 5f;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f; //the larger this is, the slower the NPC will turn
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity.X = move.X;
            #endregion

            #region Jump
            if (NPC.collideY && NPC.collideX)
            {
                if (NPC.ai[0] <= 0f)
                {
                    if (jump < 1)
                    {
                        NPC.velocity.Y = -3f;
                        NPC.ai[0] = 80f;

                        if (NPC.position.X == NPC.oldPosition.X)
                        {
                            jump++;
                        }
                        else
                        {
                            jump = 0;
                        }
                    }
                    else if (jump >= 1 && jump <= 8)
                    {
                        NPC.velocity.Y = -8f;
                        NPC.ai[0] = 80f;
                        jump = 0;
                    }
                }

                NPC.ai[0] -= 1f;
            }
            #endregion

            #region Gravity
            NPC.velocity.Y = NPC.velocity.Y - 0.1f;
            #endregion
        }

        private void Teleport(Player target)
        {
            int dir;
            NPC.ai[1]++;

            if (NPC.ai[1] >= 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                if (Main.rand.NextBool(2))
                {
                     dir = 150;
                }
                else
                {
                    dir = -150;
                }

                NPC.position.X = (target.position.X + dir);
                NPC.position.Y = target.position.Y - 30;
                NPC.ai[1] = 0;
            }

        }
    }
}
