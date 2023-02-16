using DevilsWarehouse.Content.Items.Materials;
using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Weapons.Ranged.Guns
{
    public class Mbee5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MBee5");
            Tooltip.SetDefault("Does not accept fakes");
        }
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 26;

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item31;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 6;
            Item.knockBack = 2f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, +5f);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeeWax, 20)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.IllegalGunParts, 1)
                .AddTile(TileID.Furnaces)
                .Register();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<BeeDart>();
        }
    }
}
