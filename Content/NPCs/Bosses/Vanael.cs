using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.NPCs.Bosses
{
    public class Vanael : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 120;
            NPC.lifeMax = 10000;
            NPC.boss = true;
            NPC.ai[0] = 0f;
            NPC.ai[2] = 0f;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.spriteDirection = 1;

            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - NPC.Center;

            NPC.TargetClosest();

            if (NPC.ai[0] <= 400)
            {
                double deg = (double)NPC.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 250;

                NPC.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                NPC.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;
                NPC.ai[1] += 3;

                if (NPC.ai[2] >= 20)
                {
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), NPC.position, move, ProjectileID.WoodenArrowHostile, NPC.damage, 3);
                    NPC.ai[2] = 0;
                }
                NPC.ai[2]++;
            }
            else if (NPC.ai[0] > 400 && NPC.ai[0] <= 800)
            {
                float speed = 5f;
                move = moveTo - NPC.Center;
                float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 10f;
                move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
                magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                NPC.velocity = move;
            }
            else if (NPC.ai[0] > 800 && NPC.ai[0] <= 1200)
            {
                moveTo = player.Center - new Vector2(0, 100);

                float speed = 10f;
                move = moveTo - NPC.Center;
                float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 2f;
                move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
                magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                NPC.velocity = move;
            }
            else
            {
                NPC.ai[0] = 0;
            }

            NPC.ai[0] += Main.rand.Next(3);
        }
    }
}
