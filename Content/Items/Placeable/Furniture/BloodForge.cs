using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Placeable.Furniture {
    internal class BloodForge : ModItem{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Blood Forge");
			Tooltip.SetDefault("a cursed forge to a mysterious power");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Content.Tiles.BloodForge>());
			Item.value = 150;
			Item.maxStack = 99;
			Item.width = 38;
			Item.height = 24;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		//public override void AddRecipes() {
		//	CreateRecipe()
		//		.AddIngredient(ItemID.WoodenTable)
		//		.AddIngredient(itemID)
		//		.Register();
		//}
	}
}
