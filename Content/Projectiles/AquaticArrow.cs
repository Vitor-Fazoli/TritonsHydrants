using DevilsWarehouse.Content.Dusts;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace DevilsWarehouse.Content.Projectiles
{
    public class AquaticArrow : ModProjectile
    {
        private bool empowered = false;
        private bool activation = false;
        private bool bouncyProjectile = false;

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 20;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 3;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        //<summary> when the projectile enter in the water, transform to a homing projectile </summary>
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.position, ModContent.DustType<WaterPower>(), Vector2.Zero);
            Lighting.AddLight(Projectile.position, dust.color.R / 255, dust.color.G / 255, dust.color.B / 255);
            Projectile.rotation = Projectile.velocity.ToRotation();

            #region WaterEffects


            if (Projectile.wet)
            {
                switch (Main.waterStyle)
                {
                    case 2:

                        if (activation == false)
                        {
                            Projectile.Kill();
                            float numberProjectiles = 5;
                            float rotation = MathHelper.ToRadians(45);

                            Projectile.position += Vector2.Normalize(Projectile.velocity) * 45f;
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                            }
                            activation = true;
                        }
                        break;
                    case 3:
                        Projectile.velocity *= 1.25f;
                        break;
                    case 4:
                        //OnTileCollide
                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 12:

                        break;
                    default:

                        break;
                }
            }

            if (empowered)
            {

                empowered = true;
                Projectile.ai[0]++;

                if (Projectile.ai[0] >= 30)
                {

                    HomingProjectile();

                    if (Projectile.velocity == Vector2.Zero)
                    {
                        Projectile.Kill();
                    }
                }

                if (Projectile.ai[0] <= 1)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(3f, 3f);
                        Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterPower>(), speed * 5);
                        d.noGravity = true;
                    }
                }
            }

            if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.soundDelay = 10;
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }
            #endregion

        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterPower>(), speed * 5);
                d.noGravity = true;

                Lighting.AddLight(Projectile.position, d.color.R / 255, d.color.G / 255, d.color.B / 255);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.waterStyle == 4)
            {
                Projectile.penetrate--;
                if (Projectile.penetrate <= 0)
                {
                    Projectile.Kill();
                }
                else
                {
                    Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

                    // If the projectile hits the left or right side of the tile, reverse the X velocity
                    if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    {
                        Projectile.velocity.X = -oldVelocity.X;
                    }

                    // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                    if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    {
                        Projectile.velocity.Y = -oldVelocity.Y;
                    }
                }
            }


            return false;
        }
        private void HomingProjectile()
        {
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool isTarget = false;

            for (int k = 0; k < 100; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        isTarget = true;
                    }
                }
            }

            if (isTarget)
            {
                AdjustMagnitude(ref move);
                Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                AdjustMagnitude(ref Projectile.velocity);
            }
        }
        private static void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }
    }
}