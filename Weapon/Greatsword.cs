using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Slash;

namespace GreatswordsMod.Weapon
{
    public abstract class Greatsword : ModProjectile
    {
        private bool max = false;

        protected int dmg = 10;
        protected float cooldown = 30;
        protected int proj = ModContent.ProjectileType<IronSlash>();
        protected float dmgMult = 1.5f;

        public override void AI()
        {

            Player player = Main.player[projectile.owner];
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            float speed = player.meleeSpeed / 2;
  
            projectile.velocity.Y = 0;
            projectile.velocity.X = 0;
            projectile.position.Y = player.position.Y - 62;
            projectile.direction = player.direction;
            projectile.spriteDirection = projectile.direction;


            if (player.direction > 0)
                projectile.position.X = player.position.X -74;
            else 
                projectile.position.X = player.position.X +13;
            

            if (channeling && projectile.ai[0] > cooldown)
            {
                player.velocity.X *= 0.98f;
                projectile.ai[0] = cooldown;
                
                if (max == false)
                {
                    max = true;
                    Main.PlaySound(SoundID.Item, player.position, 28);
                }
            }
            if (projectile.ai[0] <= cooldown + 2)
            {
                projectile.ai[0] += speed;
                projectile.timeLeft = 122;
            }

            player.heldProj = projectile.whoAmI;

            if (projectile.ai[1] < cooldown)
            {
                player.itemTime = (int)((45f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((45f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));

                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
            }
            else
            {
                player.itemTime = 2;
                player.itemAnimation = 2;
            }
            if (!channeling && projectile.ai[0] >= cooldown)
            {
                projectile.Kill();
                player.itemTime = 2;
                player.itemAnimation = 2;
                Main.PlaySound(SoundID.Item, player.position, 1);
                Projectile.NewProjectileDirect(player.position,Vector2.Zero,proj,(int)(dmg * dmgMult),3,projectile.owner);
            }else if(!channeling && projectile.ai[0] < cooldown)
            {
                projectile.Kill();
                player.itemTime = 2;
                player.itemAnimation = 2;
                Main.PlaySound(SoundID.Item, player.position, 1);
                Projectile.NewProjectileDirect(player.position,Vector2.Zero,proj,dmg,3,projectile.owner);
            }
        }
    }
}
