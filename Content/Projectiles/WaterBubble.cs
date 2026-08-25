using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Projectiles
{
    public class WaterBubble : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 1000;
            Projectile.alpha = 50;
            Projectile.light = 0.1f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.scale = Main.rand.NextFloat(0.7f, 1.25f);
                Projectile.localAI[0] = 1f;
            }

            Projectile.velocity.Y -= 0.005f;
            Projectile.velocity.X += (float)Math.Sin(Projectile.timeLeft * 0.08f) * 0.02f;

            Projectile.velocity.X += Main.windSpeedCurrent * 0.01f;
            Projectile.velocity *= 0.98f;

            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water, 0f, 0f, 100, default, Projectile.scale);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water, 0f, 0f, 100, default, Projectile.scale);
                dust.noGravity = true;
                dust.velocity *= 1.2f;
            }
        }
    }
}