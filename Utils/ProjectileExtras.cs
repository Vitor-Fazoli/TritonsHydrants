using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace TritonsHydrants.Utils
{
    public static class ProjectileExtras
    {
        public static void ApplyBounce(Projectile proj, Vector2 oldVelocity)
        {
            proj.penetrate--;
            if (proj.penetrate <= 0)
            {
                proj.Kill();
            }
            else
            {
                Collision.HitTiles(proj.position, proj.velocity, proj.width, proj.height);
                SoundEngine.PlaySound(SoundID.Item10, proj.position);

                // If the projectile hits the left or right side of the block, invert the velocity on the X axis
                if (Math.Abs(proj.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    proj.velocity.X = -oldVelocity.X;
                }

                // If the projectile hits the top or bottom side of the block, invert the velocity on the Y axis
                if (Math.Abs(proj.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    proj.velocity.Y = -oldVelocity.Y;
                }
            }
        }
    }
}
