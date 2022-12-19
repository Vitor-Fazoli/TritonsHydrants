using DevilsWarehouse.Common.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class MedusaScepter : Artifact
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Medusa Scepter");
            ItemID.Sets.CanGetPrefixes[Item.type] = false;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Grapefruit, 20)
                .AddIngredient(ItemID.Star, 5)
                .AddIngredient(ItemID.CopperBar, 10)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}