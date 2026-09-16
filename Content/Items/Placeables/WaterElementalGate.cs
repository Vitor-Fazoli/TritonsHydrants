using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Placeables
{
    public class WaterElementalGate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.IronCage>());
            Item.width = 12;
            Item.height = 12;
            Item.value = 3000;
        }
    }
}