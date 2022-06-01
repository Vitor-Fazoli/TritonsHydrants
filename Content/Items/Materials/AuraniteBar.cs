using Terraria.ModLoader;
using Terraria.ID;

namespace GearonArsenal.Content.Items.Materials
{
    internal class AuraniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auralite Bar");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.material = true;
            Item.rare = ItemRarityID.Green;
        }
    }
}