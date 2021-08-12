using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Abstract
{
    public abstract class Dash : ModProjectile
    {
        public bool dashActive = false;
        public override void SetDefaults()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.width = 20;
            Projectile.height = player.height;
            Projectile.timeLeft = 15;
            Projectile.tileCollide = true;
            Projectile.knockBack = 20;
            Projectile.damage = 10;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.eocDash = Projectile.timeLeft;
            player.armorEffectDrawShadowEOCShield = true;

            Projectile.velocity.Y = 0;
            player.Center = new Vector2(Projectile.Center.X + 20 * player.direction, Projectile.Center.Y);

            if (Projectile.timeLeft >= 5)
            {
                Projectile.velocity.X = 2 * player.direction * Projectile.timeLeft;
            }
            else
            {
                Projectile.velocity.X = player.direction * Projectile.timeLeft;
            }

            if (Projectile.active)
                dashActive = true;
            else
                dashActive = false; 
        }
    }
}
