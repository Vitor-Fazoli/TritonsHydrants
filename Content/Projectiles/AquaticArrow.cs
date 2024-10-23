using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Dusts;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Projectiles
{
    public class AquaticArrow : ModProjectile
    {
        private bool _enterOnWater;
        private bool _active;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 20;
            Projectile.timeLeft = 100;
            Projectile.netUpdate = true;
            Projectile.velocity *= 2;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override Color? GetAlpha(Color lightColor) => Water.GetWaterColor().MultiplyRGBA(Color.White);

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<ArcanePowder>(), Vector2.Zero);
            dust.color = Water.GetWaterColor();

            Lighting.AddLight(Projectile.position, dust.color.R / 255f, dust.color.G / 255f, dust.color.B / 255f);


            WaterEffect();

            if (Projectile.wet)
            {
                if (!_enterOnWater)
                {
                    OnWetProjectile();
                    _enterOnWater = true;
                }
                _active = true;
            }

            if (_active)
            {
                Empower();
            }

            if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.soundDelay = Helper.Ticks(1);
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }

        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<ArcanePowder>(), Main.rand.NextVector2Circular(2, 2));
                dust.color = Water.GetWaterColor();
                dust.noGravity = true;

                Lighting.AddLight(Projectile.position, Water.GetWaterColor().ToVector3());
            }
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
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<ArcanePowder>(), speed * 5);
                d.noGravity = true;
            }
        }

        private void WaterEffect()
        {
            //Player p = Main.player[Projectile.owner];

            switch (Main.waterStyle)
            {
                case Water.Jungle:
                    break;
                case Water.Desert or Water.Desert2:
                    break;
                case Water.Cavern or Water.Cavern2:
                    break;
                case Water.BloodMoon or Water.Crimsom:
                    break;
                case Water.Corruption:
                    break;
                case Water.Snow:
                    break;
            }
        }

        /// <summary>
        /// When projectile is on water, it will empower (Upgrade velocity and adding homing)
        /// </summary>
        private void Empower()
        {
            Projectile.ai[1]++;

            if (Projectile.ai[1] >= 25)
            {
                HomingProjectile();
                Projectile.velocity *= 1.0001f;
            }
        }
    }
}