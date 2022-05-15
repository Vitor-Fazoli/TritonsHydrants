using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Projectiles
{
    internal class SpinSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.damage = 100;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            Projectile.velocity *= 3;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            if (Projectile.direction >= 0)
                Projectile.rotation += 0.3f;
            else
                Projectile.rotation -= 0.3f;


            Projectile.ai[0]++;


            if(Projectile.ai[0] >= 50)
                FadeOut();
        }
        public void FadeOut()
        {
            Projectile.alpha += 25;
            
            if (Projectile.alpha > 255)
            {
                Projectile.alpha = 255;
                Projectile.Kill();
            }
        }
    }
}
