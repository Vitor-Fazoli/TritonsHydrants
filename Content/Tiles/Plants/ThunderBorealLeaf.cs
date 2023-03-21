using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Tiles.Plants
{
    public class ThunderBorealLeaf : ModGore
    {
        public override string Texture => "DevilsWarehouse/Content/Tiles/Plants/ThunderBorealTree_Leaf";
        public override void SetStaticDefaults() => GoreID.Sets.SpecialAI[Type] = 3;
    }
}
