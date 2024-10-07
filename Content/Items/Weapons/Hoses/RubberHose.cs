using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Weapons.Hoses
{
    public class RubberHose : ModItem
    {
        public override void SetDefaults()
        {
            // Item settings
            Item.damage = 10;
            Item.knockBack = 4.5f;
            Item.rare = ItemRarityID.White;
            //Item.value = Item.sellPrice(silver: 23);
            //Item.shoot = ModContent.ProjectileType<RubberHoseProj>();
            Item.useAnimation = 11;
            Item.useTime = 11;

            // Texture settings
            Item.width = 44;
            Item.height = 44;
            Item.scale = 1f;

            // Default Stats
            Item.shootSpeed = 11f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 3)
                .AddIngredient(ItemID.Wood, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}