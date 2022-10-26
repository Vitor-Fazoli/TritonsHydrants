using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using DevilsWarehouse.Common.Abstract;

namespace DevilsWarehouse.Content.Items.Weapons.Melee.Greatswords
{
    public class GoldGreatsword : ItemGreatsword
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Gold Greatsword");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {

            //properties - Default
            Item.damage = ModContent.GetInstance<GoldGreatswordP>().GetDmg();
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = ModContent.GetInstance<GoldGreatswordP>().GetKnk();
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = greatsword;
            Item.channel = true;
            Item.crit = -4;

            //properties - Item
            greatsword = ModContent.ProjectileType<GoldGreatswordP>();
            gStats = ModContent.GetInstance<GoldGreatswordP>();
            slash = ModContent.ProjectileType<GoldSlash>();
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 9)
            .AddIngredient(ItemID.CopperShortsword, 3)
            .AddIngredient(ItemID.SpookyClock, 2)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
    public class GoldGreatswordP : Greatsword
    {

        public override void SetDefaults()
        {

            //properties - Default
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 124;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.extraUpdates = 1;

            //properties - Greatsword
            gDamage = 11;
            cooldown = 73;
            aggro = 10;
            defense = 3;
            slash = ModContent.ProjectileType<GoldSlash>();
            wEffect = DustID.Cloud;
        }
    }
    public class GoldSlash : Slash
    {

        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Gold Slash");
            Main.projFrames[Projectile.type] = frames;
        }
        public override bool PreDraw(ref Color lightColor)
        {

            lightColor = new(242, 211, 108);
            return base.PreDraw(ref lightColor);
        }
    }
}