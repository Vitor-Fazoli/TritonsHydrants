using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Diagnostics.Metrics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace DevilsWarehouse.Content.NPCs.Bosses
{
    public class Yandra : ModNPC
    {
        private int counter;
        private bool arrowRain = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yandra");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 56;
            NPC.height = 78;
            NPC.lifeMax = 5000;
            NPC.boss = true;
            NPC.ai[0] = 0f;
            NPC.ai[2] = 0f;
            NPC.defense = 5;
            NPC.noTileCollide = true;
            NPC.netUpdate = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 10f;
            NPC.damage = 20;
        }
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];

            if (arrowRain)
            {
                NPC.frame.Y = 0 * frameHeight;
                NPC.spriteDirection = 1;
            }
            else if (player.position.Y <= NPC.position.Y - 50)
            {
                NPC.frame.Y = 1 * frameHeight;
                NPC.spriteDirection = NPC.direction;
            }
            else if (player.position.Y < NPC.position.Y - 50 && player.position.Y > player.position.Y + 50)
            {
                NPC.frame.Y = 2 * frameHeight;
                NPC.spriteDirection = NPC.direction;
            }
            else if (player.position.Y >= NPC.position.Y + 50)
            {
                NPC.frame.Y = 3 * frameHeight;
                NPC.spriteDirection = NPC.direction;
            }
            else
            {
                NPC.frame.Y = 2 * frameHeight;
                NPC.spriteDirection = NPC.direction;
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            counter = 0;
            NPC.lifeMax = 5000;
            NPC.NewNPC(source, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<VictimiaHead>());
        }
        public override void AI()
        {

            int actionTimer = ActionTimerDifficult();
            int damage = 10;
            Player player = Main.player[NPC.target];
            EntitySource_TileBreak entitySource = new(2, 2);
            short proj = ProjectileID.WoodenArrowHostile;
            int chooseAttacks = 1;

            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - NPC.Center;

            NPC.TargetClosest();

            if (Main.myPlayer == player.whoAmI)
            {
                NPC.netUpdate = true;
            }

            if (NPC.ai[0] <= 0)
            {
                chooseAttacks = Main.rand.Next(5);
            }

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
                    NPC.ai[1]++;
                    if (NPC.ai[2] >= 20)
                    {
                        Projectile.NewProjectile(
                            entitySource,
                            NPC.position,
                            move,
                            proj,
                            damage,
                            3,
                            Main.myPlayer,
                            NPC.whoAmI);
                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2]++;
                }
                else
                {
                    if (player.wingTimeMax > 0)
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[2] >= 20)
                        {
                            Projectile.NewProjectile(
                                entitySource,
                                NPC.position,
                                move,
                                proj,
                                damage,
                                3,
                                Main.myPlayer,
                                NPC.whoAmI);
                            NPC.ai[2] = 0;
                        }
                        NPC.ai[2] += 3;
                    }
                    else
                    {
                        NPC.ai[1]--;
                        if (NPC.ai[2] >= 20)
                        {
                            Projectile.NewProjectile(
                                entitySource,
                                NPC.position,
                                move,
                                proj,
                                damage / 2,
                                3,
                                Main.myPlayer,
                                NPC.whoAmI);
                            NPC.ai[2] = 0;
                        }
                        NPC.ai[2]++;
                    }

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
                    if (chooseAttacks <= 1)
                    {
                        Vector2 offset = new(0, -300);
                        Movement(player.Center - offset, 15, 12);

                        if (NPC.ai[2] >= actionTimer / 5)
                        {

                            NPC.HealEffect(25, true);
                            NPC.life += 25;
                            NPC.ai[2] = 0;
                        }
                        NPC.ai[2]++;
                    }
                    else
                    {

                        Vector2 offset = new(-300, 0);

                        Movement(player.Center - offset, 15, 12);

                        if (NPC.ai[2] >= 20)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Projectile.NewProjectile(
                                    entitySource,
                                    NPC.Center - new Vector2(30, 0),
                                    new Vector2(-100, 0),
                                    proj,
                                    damage,
                                    3,
                                    Main.myPlayer,
                                    NPC.whoAmI);
                            }
                            NPC.ai[2] = 0;
                        }
                        NPC.ai[2]++;
                    }
                }
                else
                {
                    Vector2 offset = new(300, 0);

                    Movement(player.Center - offset, 15, 12);

                    if (NPC.ai[2] >= 20)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Projectile.NewProjectile(
                                entitySource,
                                NPC.Center - new Vector2(30, 0),
                                new Vector2(100, 0),
                                proj,
                                damage,
                                3,
                                Main.myPlayer,
                                NPC.whoAmI);
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
                if (chooseAttacks <= 0 && player.HasItem(ItemID.IronskinPotion))
                {
                    if (counter == 0)
                    {
                        CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Center.Y - 50, 10, 10), Color.Yellow, "YOU'RE IRON SKIN IS MINE");

                        player.ConsumeItem(ItemID.IronskinPotion, true);
                        NPC.AddBuff(BuffID.Ironskin, Readability.ToTicks(60));
                        counter++;
                        NPC.ai[0] += 200;
                    }
                }
                else
                {
                    arrowRain = true;
                    if (NPC.ai[2] >= 20)
                    {
                        Vector2 velocity = new(0, -50);
                        float numberProjectiles = 3 + Main.rand.Next(3);
                        float rotation = MathHelper.ToRadians(30);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(
                                entitySource,
                                new Vector2(NPC.Center.X, NPC.Center.Y - (NPC.height / 2)),
                                perturbedSpeed,
                                proj,
                                damage,
                                6,
                                player.whoAmI,
                                NPC.whoAmI);
                        }

                        NPC.ai[2] = 0;
                    }
                    NPC.ai[2]++;
                }
            }
            else
            {
                arrowRain = false;
                counter = 0;
                NPC.ai[0] = 0;
            }

            NPC.ai[0]++;
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
                    return 300;
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
                    return 420;
                }
            }
        }
    }
}
