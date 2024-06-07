using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MagicTridents.Utils
{
    /// <summary>
    /// This class is to turn easier using Projectiles
    /// </summary>
    public static class ProjectileExtras
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="power"></param>
        /// <param name="limit"></param>
        public static float ApplyGravity(float velocityY, float power = 0.1f, float limit = 16)
        {
            if (velocityY > limit)
            {
                return limit;
            }
            else
            {
               return velocityY += power;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="oldVelocity"></param>
        public static void ApplyBounce(ModProjectile proj, Vector2 oldVelocity)
        {
            proj.Projectile.penetrate--;
            if (proj.Projectile.penetrate <= 0)
            {
                proj.Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(proj.Projectile.position, proj.Projectile.velocity, proj.Projectile.width, proj.Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, proj.Projectile.position);

                // If the Projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(proj.Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    proj.Projectile.velocity.X = -oldVelocity.X;
                }

                // If the Projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(proj.Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    proj.Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
        }
        public static void ApplyOrbitingPlayer(ModProjectile projectile, Player player, float radius, float speed)
        {
            double deg = (double)projectile.Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = radius;

            projectile.Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - projectile.Projectile.width / 2;
            projectile.Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.Projectile.height / 2;
            projectile.Projectile.ai[1] += speed;
        }
        public static void ApplyOrbitingAnything(ModProjectile projectile, Vector2 Center, float radius, float speed)
        {
            double deg = (double)projectile.Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = radius;

            projectile.Projectile.position.X = Center.X - (int)(Math.Cos(rad) * dist) - projectile.Projectile.width / 2;
            projectile.Projectile.position.Y = Center.Y - (int)(Math.Sin(rad) * dist) - projectile.Projectile.height / 2;
            projectile.Projectile.ai[1] += speed;
        }
    }
}
