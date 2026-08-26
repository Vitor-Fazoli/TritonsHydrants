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
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
            ItemID.Sets.OreDropsFromSlime[Type] = (3, 13);
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