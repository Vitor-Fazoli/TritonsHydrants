using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Materials
{
    internal class OxHorn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ox Horn");
        }
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 22;
            Item.material = true;
            Item.rare = ItemRarityID.Green;
            Item.shopCustomPrice = 1000;
            Item.stack = 99;
        }
    }
}
