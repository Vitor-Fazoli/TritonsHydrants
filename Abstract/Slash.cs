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
        private bool resultDir = false;
        #endregion
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = frames;
        }
        public override bool PreAI()
        {
            #region SetDirection
            if (!resultDir)
            {
                if (Main.MouseScreen.X > Main.screenWidth / 2)
                {
                    Projectile.spriteDirection = 1;
                }
                else
                {
                    Projectile.spriteDirection = -1;
                }
                resultDir = true;
            }
            return true;
            #endregion
        }
        public override void AI()
        {
            #region Projectile Position
            Player p = Main.player[Projectile.owner];
            Projectile.Center = p.Center;
            p.direction = Projectile.spriteDirection;   
            #endregion

            #region Animation
            if (++Projectile.frameCounter >= spdFrame)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= frames)
                {
                    Projectile.frame = 0;
                    Projectile.Kill();
                }
            }
            #endregion
        }
    }
}
