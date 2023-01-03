using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

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
            NPC.defense = 20;
        }

        public override void AI()
        {
            int actionTimer = ActionTimerDifficult(); 

            Player player = Main.player[NPC.target];
            EntitySource_TileBreak entitySource = new(2, 2);
            short proj = ProjectileID.WoodenArrowHostile;

            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - NPC.Center;

            NPC.TargetClosest();



            if (NPC.ai[0] <= actionTimer) //BayBlade
            {
                double deg = (double)NPC.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 250;

                //NPC.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                //NPC.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;

                Movement(new Vector2(player.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2,
                                     player.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2), 9, 8);

                if (player.statLife >= player.statLifeMax2 / 2)
                {
                    NPC.ai[1]--;
                    if (NPC.ai[2] >= 20)
                    {
                        Projectile.NewProjectile(entitySource, NPC.position, move, proj, NPC.damage, 3);
                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2]++;
                }
                else
                {
                    NPC.ai[1]+= 2;
                    if (NPC.ai[2] >= 20)
                    {
                        Projectile.NewProjectile(entitySource, NPC.position, move, proj, (int)(NPC.damage * 1.5f), 3);
                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2] += 2;
                }
            }
            else if (NPC.ai[0] > actionTimer && NPC.ai[0] <= (actionTimer * 2)) //Tiros pela lateral
            {
                if (player.statDefense > NPC.defense && NPC.life <= NPC.lifeMax / 2)
                {
                    proj = ProjectileID.PaladinsHammerHostile;
                }
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    Vector2 offset = new(0, -300);
                    Movement(player.Center - offset, 15, 12);

                    if (NPC.ai[2] >= actionTimer / 5)
                    {

                        NPC.HealEffect(100, true);
                        NPC.life += 100;
                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2]++;
                }
                else
                {
                    Vector2 offset = new(300, 0);

                    Movement(player.Center - offset, 15, 12);

                    if (NPC.ai[2] >= 20)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center - new Vector2(30, 0), new Vector2(100, 0), proj, NPC.damage, 3);
                        }
                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2]++;
                }
            }
            else if (NPC.ai[0] > (actionTimer * 2) && NPC.ai[0] <= (actionTimer * 3)) //Tiro pra cima
            {
                Vector2 offset = new(0, 200);

                Movement(player.Center - offset, 8, 10);
                if (NPC.ai[1] <= 0)
                {
                    if(player.HasItem(ItemID.IronskinPotion))
                    {
                        if (NPC.position.Between(player.position - offset,player.position + offset))
                        {
                            
                        }
                    }
                }
                else if (NPC.ai[2] >= 20)
                {
                    Vector2 velocity = new(0, -50);
                    float numberProjectiles = 3 + Main.rand.Next(5);
                    float rotation = MathHelper.ToRadians(30);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                        Projectile.NewProjectile(entitySource, new Vector2(NPC.Center.X, NPC.Center.Y - (NPC.height / 2)), perturbedSpeed, proj, 20, 6, player.whoAmI);
                    }

                    NPC.ai[2] = 0;
                }
                NPC.ai[2]++;
                NPC.ai[1]--;
            }
            else
            {
                NPC.ai[0] = 0;
            }

            NPC.ai[0] += Main.rand.Next(3);
        }
        private void Movement(Vector2 moveTo, float speed, float turnResistance)
        {
            Vector2 move = moveTo - NPC.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);

            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }
        private static int ActionTimerDifficult()
        {
            if (Main.expertMode)
            {
                if (Main.hardMode)
                {
                    return 420;
                }
                else
                {
                    return 360;
                }
            }
            else
            {
                if (Main.hardMode)
                {
                    return 360;
                }
                else
                {
                    return 300;
                }
            }
        }
    }
}
