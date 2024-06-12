using MagicTridents.Content.Dusts;
using MagicTridents.Utils;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MagicTridents.Content.Projectiles
{
    //TODO: CRIMSOM -> Projétil Rouba vida
    //TODO: CORRUPTION -> Pensar em algo
    //TODO: Blood MOON -> Projétil Rouba vida

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
            Projectile.netUpdate = true;
            Projectile.velocity *= 2;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        //<summary> when the projectile enter in the water, transform to a homing projectile </summary>
        public override void OnSpawn(IEntitySource source)
        {
            switch (Main.waterStyle)
            {
                case Water.Hallow:
                    Projectile.NewProjectileDirect(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity.RotatedBy(0.261799), ModContent.ProjectileType<AquaticShard>(), Projectile.damage / 3, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectileDirect(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity.RotatedBy(-0.261799), ModContent.ProjectileType<AquaticShard>(), Projectile.damage / 3, Projectile.knockBack, Projectile.owner);
                    break;
                case Water.Jungle:
                    Projectile.penetrate = 5;
                    break;
            }

            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
            happen = false;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.position, ModContent.DustType<WaterBubble>(), Vector2.Zero);
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
                Projectile.soundDelay = Helper.Ticks(1);
                SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
            }

        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterBubble>(), speed * 5);
                d.noGravity = true;

                Lighting.AddLight(Projectile.position, Water.GetWaterColor().ToVector3());
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player p = Main.player[Projectile.owner];

            if (target.life <= 0 && (Main.waterStyle is Water.Crimsom || Main.waterStyle is Water.BloodMoon))
            {
                p.Heal(Projectile.damage / 10);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.waterStyle is Water.Jungle)
            {
                ProjectileExtras.ApplyBounce(this, oldVelocity);
                return false;
            }

            return base.OnTileCollide(oldVelocity);
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
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterBubble>(), speed * 5);
                d.noGravity = true;
            }
        }
        private void WaterEffect()
        {
            Player p = Main.player[Projectile.owner];

            float frequency = 0.2f; // Frequência do movimento senoidal
            float amplitude = 2f; // Amplitude do movimento senoidal

            switch (Main.waterStyle)
            {
                case Water.Jungle:
                    Projectile.tileCollide = true;
                    Projectile.velocity.Y = ProjectileExtras.ApplyGravity(Projectile.velocity.Y);
                    break;
                case Water.Desert:
                    ProjectileExtras.ApplyOrbitingPlayer(this, p, 64, 1.5f);
                    Projectile.tileCollide = false;
                    break;
                case Water.Desert2:
                    ProjectileExtras.ApplyOrbitingPlayer(this, p, 64, 1.5f);
                    Projectile.tileCollide = false;
                    break;
                case Water.Cavern:
                    Projectile.tileCollide = false;
                    Projectile.position.Y += (float)Math.Sin(Projectile.timeLeft * frequency) * amplitude;
                    break;
                case Water.Cavern2:
                    Projectile.tileCollide = false;
                    Projectile.position.Y += (float)Math.Sin(Projectile.timeLeft * frequency) * amplitude;
                    break;
                case Water.Snow:
                    Projectile.tileCollide = false;
                    Projectile.aiStyle = ProjAIStyleID.Boomerang;
                    Projectile.timeLeft = 1200;
                    Projectile.penetrate = 200;
                    break;
            }
        }
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