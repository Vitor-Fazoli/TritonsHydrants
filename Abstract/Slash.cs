using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using GearonArsenalMod.Buffs;

namespace GearonArsenalMod.Abstract
{
    public class Slash : ModProjectile
    {
        #region Slash Attributes
        public override string Texture => ("GearonArsenalMod/Abstract/Slash");
        protected int frames = 11;
        protected int spdFrame = 2;
        private bool resultDir = false;
        private bool hit = false;
        #endregion
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = frames;
        }
        public override void SetDefaults()
        {
            Projectile.width = 400;
            Projectile.height = 200;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
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

            #region Speed Projectile
            Vector2 moveTo = Main.MouseWorld;
            float speedProj = 12f;
            Vector2 move = moveTo - Projectile.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speedProj / magnitude;
            }
            float turnResistance = 5f;
            move = (Projectile.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speedProj)
            {
                move *= speedProj / magnitude;
            }
            Projectile.velocity.X = move.X;
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

            if (hit)
                Projectile.damage = 0;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Height = 100;
            hitbox.Y += 50;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!hit)
                target.AddBuff(ModContent.BuffType<Marked>(),120);

            hit = true;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if(!hit)
                target.AddBuff(ModContent.BuffType<Marked>(), 120);

            hit = true;
        }
    }
}
