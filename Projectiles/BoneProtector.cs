using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using GearonArsenalMod.Buffs;

namespace GearonArsenalMod.Projectiles
{
    public class BoneProtector : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Player p = Main.player[Projectile.owner];
            Projectile.friendly = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.knockBack = 10;
            Projectile.timeLeft = 1800;
            Projectile.damage = p.statDefense / 5;
        }
        public override void AI()
        {
            Player p = Main.player[Projectile.owner];

            double degree = Projectile.ai[1];
            double radian = degree * (Math.PI / 180);
            double distance = 100;

            if (p.ownedProjectileCounts[ModContent.ProjectileType<BoneProtector>()] <= 1 && Projectile.ai[1] <= 10)
            {
                int dust = DustID.Bone;

                SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal.SoundId,p.position,0);
                int num0 = Dust.NewDust(new Vector2(p.position.X, p.Center.Y), (p.width / 2), (p.height / 5), dust, +5f, 0,100,new(58, 50, 85));
                Main.dust[num0].scale = 1f;
                Main.dust[num0].noGravity = true;

                int num1 = Dust.NewDust(new Vector2(p.position.X, p.Center.Y), (p.width / 2), (p.height / 5), dust, -5f, 0, 100, new(58, 50, 85));
                Main.dust[num1].scale = 1f;
                Main.dust[num1].noGravity = true;

                int num2 = Dust.NewDust(new Vector2(p.position.X, p.Center.Y), (p.width / 2), (p.height / 5), dust, 0, -5f, 100, new(58, 50, 85));
                Main.dust[num2].scale = 1f;
                Main.dust[num2].noGravity = true;

                int num3 = Dust.NewDust(new Vector2(p.position.X, p.Center.Y), (p.width / 2), (p.height / 5), dust, 0, +5f, 100, new(58, 50, 85));
                Main.dust[num3].scale = 1f;
                Main.dust[num3].noGravity = true;
            }

            Projectile.rotation += 0.75f;

            Vector2 orb = new (p.Center.X - (float)(Math.Cos(radian) * distance) - Projectile.width / 2, p.Center.Y - (float)(Math.Sin(radian) * distance) - Projectile.height / 2);

            Projectile.position = orb;

            if(Projectile.ai[1] == 120 && p.ownedProjectileCounts[ModContent.ProjectileType<BoneProtector>()] < 2)
            {
                Projectile.NewProjectileDirect(new ProjectileSource_TileBreak(2, 2), orb, Vector2.Zero, ModContent.ProjectileType<BoneProtector>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            if (Projectile.ai[1] == 240 && p.ownedProjectileCounts[ModContent.ProjectileType<BoneProtector>()] < 3)
            {
                Projectile.NewProjectileDirect(new ProjectileSource_TileBreak(2, 2), orb, Vector2.Zero, ModContent.ProjectileType<BoneProtector>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            Projectile.ai[1]+= 3;
        }
    }
}
