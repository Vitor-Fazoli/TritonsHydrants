using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace VoidArsenal.Content.Items.Materials
{
    public class DemonHoneyIngot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DemonHoney Ingot");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.material = true;
            Item.rare = ItemRarityID.Blue;
            Item.shopCustomPrice = 1000;
            Item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 2)
                .AddIngredient(ItemID.BeeWax, 5)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
