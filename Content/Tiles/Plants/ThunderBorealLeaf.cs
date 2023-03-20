using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Tiles.Plants
{
    public class ThunderBorealLeaf : ModGore
    {
        public override string Texture => "ExampleMod/Content/Tiles/Plants/ExampleTree_Leaf";
        public override void SetStaticDefaults() => GoreID.Sets.SpecialAI[Type] = 3;
    }
}
