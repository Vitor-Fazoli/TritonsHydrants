using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GreatswordsMod.Abstract
{
    public abstract class Greatsword : ModProjectile
    {
        private bool soundBreak = false;

        #region Greatsword Attributes
        protected int dmg = 10;
        protected int knk = 3;
        protected float cooldown = 30;
        protected int proj = ModContent.ProjectileType<Item.CopperSlash>();
        protected int grtCritical;
        protected int wEffect = 16;
        protected float velPlayer = 0;
        protected int timeMax = 10;
        protected int rare = 1;
        #endregion

        public override void SetDefaults()
        {

        }
        public override void Load()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position.Y = player.position.Y - 62;
        }
        public override void AI()
        {
            #region Basic Attributes
            Player player = Main.player[Projectile.owner];
            GreatPlayer modPlayer = player.GetModPlayer<GreatPlayer>();
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            float speed = player.meleeSpeed / 2;
            grtCritical = modPlayer.slayerPower * 10;
            #endregion

            #region Projectile Position

            //Direction
            if (Main.MouseScreen.X > Main.screenWidth / 2)
            {
                Projectile.spriteDirection = 1;
                player.direction = Projectile.spriteDirection;
            }
            else
            {
                Projectile.spriteDirection = -1;
                player.direction = Projectile.spriteDirection;
            }
            
            //Velocity
            Vector2 moveTo;

            moveTo.Y = player.position.Y - 62;

            if (player.direction > 0)
            {
                Projectile.position.X = player.position.X - 74;
                moveTo.X = player.position.X - 74;
            }
            else
            {
                Projectile.position.X = player.position.X + 13;
                moveTo.X = player.position.X + 13;
            }


            float speedProj = 25f;
            Vector2 move = moveTo - Projectile.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speedProj / magnitude;
            }
            float turnResistance = 2f;
            move = (Projectile.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speed / magnitude;
            }
            Projectile.velocity.Y = move.Y;
            #endregion

            #region Channeling
            if (channeling && Projectile.ai[0] >= cooldown)
            {
                player.velocity.X *= 0.99f * (1 + velPlayer);

                dustEffect(wEffect);

                if (soundBreak == false)
                {
                    soundBreak = true;
                    SoundEngine.PlaySound(SoundID.Item,player.position, 28);
                }

                if (Projectile.ai[0] >= cooldown + (timeMax * 2))
                {
                    Projectile.ai[0] = cooldown + (timeMax * 2);
                }
            }

            if (Projectile.ai[0] <= cooldown + (timeMax * 2))
            {
                Projectile.ai[0] += speed;
                Projectile.timeLeft = 122;
            }

            player.heldProj = Projectile.whoAmI;

            if (Projectile.ai[1] < cooldown)
            {
                player.itemTime = (int)((45f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((45f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));

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
            if (!channeling)
            {
                if (Projectile.ai[0] < cooldown)
                {
                    modPlayer.slayerPower = 0;
                    AnimationSlash(2, 1);
                    ProjectileSlash(dmg, proj, knk);
                }
                else if (Projectile.ai[0] > cooldown && Projectile.ai[0] < cooldown + timeMax)
                {
                    modPlayer.slayerPower++;
                    AnimationSlash(2, 60);
                    ProjectileSlash(dmg, proj, knk * 2);
                }
                else if (Projectile.ai[0] >= cooldown + timeMax)
                {
                    modPlayer.slayerPower = 0;
                    AnimationSlash(42, 222);
                    ProjectileSlash(dmg, proj, knk);
                }
            }
            #endregion
        }
        private void AnimationSlash(int idSound, int type)
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Kill();
            player.itemTime = 2;
            player.itemAnimation = 2;
            SoundEngine.PlaySound(idSound, player.position, type);
        }
        private void ProjectileSlash(int damage, int projectileVar, int knock)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectileDirect(new ProjectileSource_TileBreak(2,2), player.position, Vector2.Zero, projectileVar, damage, knock, Projectile.owner);
        }
        private void dustEffect(int idDust)
        {
            Player player = Main.player[Projectile.owner];

            int num1 = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + 17), (player.width / 2), (player.height / 5), idDust, +5f, -1);
            Main.dust[num1].scale = 1f;
            Main.dust[num1].noGravity = true;

            int num2 = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + 17), (player.width / 2), (player.height / 5), idDust, -5f, -1);
            Main.dust[num2].scale = 1f;
            Main.dust[num2].noGravity = true;
        }
        public int GetDmg()
        {
            return dmg;
        }
        public int GetKnk()
        {
            return knk;
        }
        public int GetCrit() {
            return grtCritical;
        }
    }
}