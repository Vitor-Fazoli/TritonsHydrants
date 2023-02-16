using DevilsWarehouse.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles
{
    public class AquaticArrow : ModProjectile
    {
        private bool EnterOnWater = false;
        private bool happen = false;
        private bool active = false;

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 20;
            Projectile.timeLeft = 600;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        //<summary> when the projectile enter in the water, transform to a homing projectile </summary>
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.Kill();
            }

            Dust dust = Dust.NewDustPerfect(Projectile.position, ModContent.DustType<WaterPower>(), Vector2.Zero);
            Lighting.AddLight(Projectile.position, dust.color.R / 255, dust.color.G / 255, dust.color.B / 255);
            Projectile.rotation = Projectile.velocity.ToRotation();

            WaterEffect();

            if (Projectile.wet)
            {
                if (!EnterOnWater)
                {
                    OnWetProjectile();
                    EnterOnWater = true;
                }
                active = true;
            }

            if (active)
            {
                Empower();
            }

            if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.soundDelay = 20;
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }

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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.waterStyle == Water.Crimsom ||
                Main.waterStyle == Water.BloodMoon)
            {

            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.waterStyle == Water.Hallow)
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

        private void OnWetProjectile()
        {
            #region Dust Effect
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterPower>(), speed * 5);
                d.noGravity = true;
            }
            #endregion
        }
        private void WaterEffect()
        {
            switch (Main.waterStyle)
            {
                case Water.Corruption:
                    //Shotgun
                    if (happen == false)
                    {
                        Projectile.Kill();
                        float numberProjectiles = 5;
                        float rotation = MathHelper.ToRadians(30);

                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, perturbedSpeed * 2, ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        }
                        happen = true;
                    }
                    break;
                case Water.Jungle:
                    //Spike Mode
                    Projectile.ai[0]++;
                    Projectile.velocity /= 5;

                    if (Projectile.ai[0] >= 60)
                    {
                        Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, new Vector2(0, 5), ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, new Vector2(0, -5), ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, new Vector2(5, 0), ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, new Vector2(-5, 0), ModContent.ProjectileType<AquaticShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.ai[0] = 0;
                    }
                    break;
                case Water.Hallow:
                    //Reflect Projectile when colide with tiles
                    Projectile.penetrate = 3;
                    break;
                case Water.Snow:
                    //Cause On fire
                    break;
                case Water.Desert:
                    //when hit a npc, gain a buff
                    break;
                case Water.Cavern:

                    break;
                case Water.Cavern2:

                    break;
                case Water.BloodMoon:
                    //LifeSteal
                    break;
                case Water.Crimsom:
                    //LifeSteal
                    break;
                case Water.Desert2:
                    //when hit a npc, gain a buff
                    break;
                default:

                    break;
            }
        }
        private void Empower()
        {
            Projectile.ai[1]++;

            if (Projectile.ai[1] >= 25)
            {
                HomingProjectile();
            }
        }
    }
}