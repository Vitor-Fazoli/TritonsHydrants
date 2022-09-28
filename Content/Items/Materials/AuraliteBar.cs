using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace DevilsWarehouse.Content.Items.Materials
{
    internal class AuraliteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auralite Bar");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.material = true;
            Item.rare = ItemRarityID.Green;
            Item.shopCustomPrice = 1000;
            Item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AuraliteOre>(6)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}