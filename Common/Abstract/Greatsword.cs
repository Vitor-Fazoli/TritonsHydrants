using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Items.Weapons.Melee.Greatswords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common.Abstract
{
    public abstract class Greatsword : ModProjectile
    {
        public override string Texture => (GetType().Namespace + "." + Name.Remove(Name.Length - 1, 1)).Replace('.', '/');

        private bool soundBreak = false;

        #region Greatsword Attributes
        protected int gDamage;
        protected int gKnockback = 1;
        protected float cooldown;
        protected int slash = ModContent.ProjectileType<Slash>();
        protected int wEffect = 16;
        protected float speedPlayer = 0;
        protected int timeMax = 10;
        public int aggro = 10;
        public int defense = 4;
        public int buff = ModContent.BuffType<WarriorWraith>();
        protected Color color = default;
        #endregion

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.DamageType = DamageClass.Melee;
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
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            float speed = player.GetAttackSpeed(DamageClass.Melee) / 2;
            #endregion

            #region Projectile Position

            //Direction
            if (Main.MouseScreen.X > Main.screenWidth / 2)
            {
                Projectile.spriteDirection = -1;
                player.direction = -Projectile.spriteDirection;
            }
            else
            {
                Projectile.spriteDirection = 1;
                player.direction = -Projectile.spriteDirection;
            }

            //Velocity
            Vector2 moveTo;

            if (player.gravity > 0)
            {
                moveTo.Y = player.position.Y + 100;
            }
            else
            {
                moveTo.Y = player.position.Y - 62;
            }

            if (player.direction > 0)
            {
                Projectile.position.X = player.position.X - 74;
                moveTo.X = player.position.X + 13;
            }
            else
            {
                Projectile.position.X = player.position.X + 13;
                moveTo.X = player.position.X + 13;
            }


            float speedProj = 100f;
            Vector2 move = moveTo - Projectile.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speedProj / magnitude;
            };
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speedProj / magnitude;
            }
            Projectile.velocity.Y = move.Y;
            #endregion

            #region Channeling
            if (channeling && Projectile.ai[0] >= (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)))
            {
                DustEffect(wEffect);

                player.velocity.X *= 0.99f * (1 + speedPlayer);


                if (soundBreak == false)
                {
                    soundBreak = true;
                }

                if (Projectile.ai[0] >= (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)) + (timeMax * 2))
                {
                    Projectile.ai[0] = (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)) + (timeMax * 2);
                }
            }

            if (Projectile.ai[0] <= (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)) + (timeMax * 2))
            {
                Projectile.ai[0] += speed;
                Projectile.timeLeft = 122;
            }

            player.heldProj = Projectile.whoAmI;

            if (Projectile.ai[1] < (int)cooldown * player.GetAttackSpeed(DamageClass.Melee))
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

                if (Projectile.ai[0] <= (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)))
                {
                    modPlayer.slayerPower = 0;
                    AnimationSlash(2, 1);
                    ProjectileSlash((int)((gDamage * 0.6) * player.GetDamage(DamageClass.Melee).Multiplicative), slash, gKnockback);
                }
                else if (Projectile.ai[0] > (int)(cooldown * player.GetAttackSpeed(DamageClass.Melee)))
                {
                    modPlayer.slayerPower++;
                    AnimationSlash(2, 60);
                    ProjectileSlash((int)((gDamage * 2.5) * (float)player.GetDamage(DamageClass.Melee).Multiplicative), slash, gKnockback * 7);
                }
            }
            #endregion
        }
        public override void Kill(int timeLeft)
        {
            Projectile.alpha = 50;
        }

        #region functions of Greatsword
        private void AnimationSlash(int idSound, int type)
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Kill();
            player.itemTime = 2;
            player.itemAnimation = 2;
        }
        private void ProjectileSlash(int damage, int projectileVar, int knock)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectileDirect(new EntitySource_TileBreak(2, 2), player.position, Vector2.Zero, projectileVar, damage, knock, player.whoAmI);
        }
        private void DustEffect(int idDust)
        {
            Player player = Main.player[Projectile.owner];


            int num1 = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + 17), (player.width / 2), (player.height / 5), idDust, +5f, -1, 100, color);
            Main.dust[num1].scale = 1f;
            Main.dust[num1].noGravity = true;

            int num2 = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + 17), (player.width / 2), (player.height / 5), idDust, -5f, -1, 100, color);
            Main.dust[num2].scale = 1f;
            Main.dust[num2].noGravity = true;
        }
        #endregion

        public int GetDmg()
        {
            return gDamage;
        }
        public int GetKnk()
        {
            return gKnockback;
        }
        public float GetCooldown()
        {
            return cooldown;
        }
    }
}