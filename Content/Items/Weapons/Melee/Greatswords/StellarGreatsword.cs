using GearonArsenal.Common;
using GearonArsenal.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Weapons.Melee.Greatswords
{
    public class StellarGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Greatsword");
        }
        public override void SetDefaults()
        {
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 10;
            Item.crit = 6;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<StellarGreatswordP>();
            Item.shootSpeed = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
        }
    }
    internal class StellarGreatswordP : ModProjectile
    {
        public int distance = 50;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Greatsword");
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


            if (Projectile.ai[0] >= distance)
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