using Terraria.ModLoader;
using Terraria.ID;

namespace GearonArsenal.Content.Items.Materials
{
    internal class AuraliteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auralite Ore");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.material = true;
            Item.rare = ItemRarityID.White;
        }
    }
}