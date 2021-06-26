using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace GreatswordsMod.Slash
{
    public class IronSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 200;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {

            Player p = Main.player[projectile.owner];
            projectile.Center = p.Center;
            projectile.spriteDirection = p.direction;

            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.frame = 0;
                    projectile.Kill();
                }
            }
        }
    }
}
