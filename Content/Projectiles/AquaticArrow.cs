using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Projectiles
{
    public class AquaticArrow : ModProjectile
    {
        private bool _enterOnWater;
        private bool _active;
        private int _initialWaterStyle;
        private bool _lifeSteal = false;
        private bool _bouncer = false;
        private bool _freezeOnThird = false;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 20;
            Projectile.timeLeft = 200;
            Projectile.netUpdate = true;
            Projectile.velocity *= 1.05f;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            WaterEffect(Projectile);
            _initialWaterStyle = Main.waterStyle;
        }

        public override Color? GetAlpha(Color lightColor) => Water.GetWaterColor().MultiplyRGBA(Color.White);

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            for (int i = 0; i < 10; i++)
            {
                Vector2 dustVelocity = new Vector2(2, 2).RotatedByRandom(100) * Main.rand.NextFloat(0.1f, 0.8f);

                Dust dust = Dust.NewDustPerfect(Projectile.Center + dustVelocity, Main.rand.NextBool(4) ? 264 : 66, dustVelocity, 0, default, Main.rand.NextFloat(0.9f, 1.2f));
                dust.noGravity = true;
                dust.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }

            Lighting.AddLight(Projectile.position, Water.GetWaterColor().ToVector3());

            OnWaterChange(Projectile);

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
                Projectile.soundDelay = TritonsHelper.Ticks(1);
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 dustVelocity = new Vector2(2, 2).RotatedByRandom(100) * Main.rand.NextFloat(0.1f, 0.8f);

                Dust dust = Dust.NewDustPerfect(Projectile.Center + dustVelocity, Main.rand.NextBool(4) ? 264 : 66, dustVelocity, 0, default, Main.rand.NextFloat(0.9f, 1.2f));
                dust.noGravity = true;
                dust.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (_bouncer)
            {
                TritonsProjectiles.ApplyBounce(Projectile, oldVelocity);
            }

            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (target.life <= 0 && _lifeSteal)
            {
                player.statLife += 5;
                player.HealEffect(5);
            }

            if (_freezeOnThird)
            {
                AquaticArrowP p = player.GetModPlayer<AquaticArrowP>();
                p.amountToFreeze++;

                if (p.amountToFreeze >= 3)
                {
                    target.AddBuff(BuffID.Frostburn, 30);
                    p.amountToFreeze = 0;
                }
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
            for (int i = 0; i < 35; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1.2f, 1.2f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(4) ? 264 : 66, speed * 5);
                d.noGravity = true;
            }
        }

        private void WaterEffect(Projectile projectile)
        {
            switch (Main.waterStyle)
            {
                case Water.Hallow:
                    HallowEffect(projectile);
                    break;
                case Water.Jungle:
                    JungleEffect(projectile);
                    break;
                case Water.Desert or Water.Desert2:
                    DesertEffect(projectile);
                    break;
                case Water.Cavern or Water.Cavern2:
                    CavernEffect(projectile);
                    break;
                case Water.BloodMoon or Water.Crimsom:
                    CrimsomEffect(projectile);
                    break;
                case Water.Corruption:
                    CorruptionEffect(projectile);
                    break;
                case Water.Snow:
                    SnowEffect(projectile);
                    break;
                default:
                    ForestEffect(projectile);
                    break;
            }
        }

        public virtual void HallowEffect(Projectile projectile)
        {
            projectile.velocity *= 1.5f;
        }

        public virtual void JungleEffect(Projectile projectile)
        {
            _bouncer = true;
        }

        public virtual void DesertEffect(Projectile projectile)
        {
            projectile.velocity /= 2.5f;
            projectile.damage *= 2;
        }

        public virtual void CavernEffect(Projectile projectile)
        {
            projectile.light = 0.5f;
            projectile.tileCollide = false;
        }

        public virtual void CrimsomEffect(Projectile projectile)
        {
            _lifeSteal = true;
        }

        public virtual void CorruptionEffect(Projectile projectile)
        {
            projectile.penetrate = 5;
        }

        public virtual void SnowEffect(Projectile projectile)
        {
            _freezeOnThird = true;
        }

        public virtual void ForestEffect(Projectile projectile)
        {
            projectile.knockBack *= 1.1f;
        }

        /// <summary>
        /// Happens when the water style changes in the world
        /// </summary>
        /// <param name="projectile">the projectile to change</param>
        private void OnWaterChange(Projectile projectile)
        {
            if (_initialWaterStyle != Main.waterStyle)
            {
                WaterEffect(projectile);
                _initialWaterStyle = Main.waterStyle;
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

    /// <summary>
    /// 
    /// </summary>
    public class AquaticArrowP : ModPlayer
    {
        public int amountToFreeze = 0;
    }
}