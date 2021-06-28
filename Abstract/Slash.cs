using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace GreatswordsMod.Abstract
{
    public abstract class Slash : ModProjectile
    {
        #region Slash Attributes
        protected int frames = 5;
        protected int spdFrame = 4;
        #endregion
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = frames;
        }
        public override void AI()
        {
            #region Projectile Position
            Player p = Main.player[projectile.owner];
            projectile.Center = p.Center;
            projectile.spriteDirection = p.direction;
            #endregion

            #region Animation
            if (++projectile.frameCounter >= spdFrame)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= frames)
                {
                    projectile.frame = 0;
                    projectile.Kill();
                }
            }
            #endregion
        }
    }
}
