using DevilsWarehouse.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace DevilsWarehouse.Common
{
    internal class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.AdamantiteBar);
            recipe.AddIngredient(ModContent.ItemType<AdamantiteFragment>());
            recipe.AddIngredient(ItemID.Meteorite, 2);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}
