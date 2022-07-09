using VoidArsenal.Common;
using VoidArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Weapons.Melee.Greatswords
{
    public class VictusYon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victus and Yon");
        }
        public override void SetDefaults()
        {
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 10;
            Item.crit = 6;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<Victus>();
            Item.shootSpeed = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)//Sets what happens on right click(special ability)
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.damage = 10;
                Item.crit = 6;
                Item.rare = ItemRarityID.Red;
                Item.shoot = ModContent.ProjectileType<Yon>();
                Item.shootSpeed = 6;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.DamageType = DamageClass.Throwing;

            }
            else
            {
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 10;
                Item.crit = 6;
                Item.rare = ItemRarityID.Red;
                Item.shoot = ModContent.ProjectileType<Victus>();
                Item.shootSpeed = 6;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.DamageType = DamageClass.Throwing;
            }
            return base.CanUseItem(player);
        }
    }
    internal class Victus : ModProjectile
    {
        public int distance = 50;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victus");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 24;
            Projectile.damage = 100;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            Projectile.velocity *= 3;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Gold, speed * 5);
                d.noGravity = true;
            }
        }
    }
    internal class Yon : ModProjectile
    {
        public int distance = 50;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yon");
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