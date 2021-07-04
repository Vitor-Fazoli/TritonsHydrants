using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Attack;

namespace GreatswordsMod.Abstract
{
    public abstract class Greatsword : ModProjectile
    {
        //Local Variable
        private bool max = false;

        #region Greatsword Attributes
        protected int dmg = 10;
        protected float cooldown = 30;
        protected int proj = ModContent.ProjectileType<IronSlash>();
        protected float dmgMult = 1.5f;
        protected int wEffect = 16;
        #endregion
        public override void AI()
        {
            #region Basic Attributes
            Player player = Main.player[projectile.owner];
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            float speed = player.meleeSpeed / 2;
            #endregion

            #region Projectile Position
            projectile.velocity.Y = 0;
            projectile.velocity.X = 0;
            projectile.position.Y = player.position.Y - 62;
            #endregion

            #region Projectile Direction
            projectile.direction = player.direction;
            projectile.spriteDirection = projectile.direction;

            if (player.direction > 0)
                projectile.position.X = player.position.X -74;
            else 
                projectile.position.X = player.position.X +13;
            #endregion

            #region Channeling
            if (channeling && projectile.ai[0] > cooldown)
            {
                player.velocity.X *= 0.98f;
                projectile.ai[0] = cooldown;

                //right
                int num1 = Dust.NewDust(new Vector2(player.position.X,player.Center.Y + 17),(player.width/2),(player.height/5), wEffect, +5f,-1);
                Main.dust[num1].scale = 1f;
                Main.dust[num1].noGravity = true;

                //left
                int num2 = Dust.NewDust(new Vector2(player.position.X,player.Center.Y + 17),(player.width/2), (player.height/5),wEffect,-5f,-1);
                Main.dust[num2].scale = 1f;
                Main.dust[num2].noGravity = true;

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
            #endregion

            #region End of Channeling / Projectile Kill
            if (!channeling && projectile.ai[0] >= cooldown)
            {
                projectile.Kill();
                player.itemTime = 2;
                player.itemAnimation = 2;
                Main.PlaySound(SoundID.Item, player.position, 60);
                Projectile.NewProjectileDirect(player.position,Vector2.Zero,proj,(int)(dmg * dmgMult),3,projectile.owner);
            }else if(!channeling && projectile.ai[0] < cooldown)
            {
                projectile.Kill();
                player.itemTime = 2;
                player.itemAnimation = 2;
                Main.PlaySound(SoundID.Item, player.position, 1);
                Projectile.NewProjectileDirect(player.position,Vector2.Zero,proj,dmg,3,projectile.owner);
            }
            #endregion
        }
    }
}
