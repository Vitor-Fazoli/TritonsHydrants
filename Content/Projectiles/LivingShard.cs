using DevilsWarehouse.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles
{
    public class LivingShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.damage = 1;
        }
        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            Projectile proj = Main.projectile[ModContent.GetInstance<LivingCore>().projIndex];

            #region Active check

            if (!proj.active)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 2;
            }
            #endregion

            #region Movement
            NPC closestNPC = Helper.FindClosestNPC(600f, Projectile);

            if (p.ownedProjectileCounts[Type] < 3 * p.numMinions || closestNPC == null)
            {
                double deg = Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 30;

                Projectile.position.X = proj.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = proj.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                Projectile.ai[1] += 2f;
                Projectile.width = 0;
                Projectile.height = 0;
            }
            else
            {
                float projSpeed = 10f;

                
                if (closestNPC == null)
                    return;

                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            #endregion

            Lighting.AddLight(Projectile.Center, new Color(63, 206, 218).ToVector3() * 0.90f);
        }
    }
}

