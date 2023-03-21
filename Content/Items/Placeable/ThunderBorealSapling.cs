
using DevilsWarehouse.Content.Tiles.Plants;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Placeable
{
    public class ThunderBorealSapling : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Example Herb Seeds");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.placeStyle = 0;
            Item.width = 12;
            Item.height = 14;
            Item.value = 80;
            Item.createTile = ModContent.TileType<ThunderBorealSaplingTile>();
        }
    }
}
