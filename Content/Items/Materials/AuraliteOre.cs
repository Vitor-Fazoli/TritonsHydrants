using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Materials
{
    internal class AuraliteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.material = true;
            Item.rare = ItemRarityID.White;
            Item.createTile = ModContent.TileType<Content.Tiles.Ores.AuraliteOre>();
            Item.maxStack = 999;
            Item.value = Item.sellPrice(silver: 1);
        }
    }
}