using DevilsWarehouse.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles
{
    public class WaterPortal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Portal");
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            #region Active check
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<IceOrbBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<IceOrbBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            #endregion

            #region Movement
            Player p = Main.player[Projectile.owner];

            double deg = (double)Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 200;

            Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.ai[1] += 0.5f;
            #endregion
        }
    }
}
