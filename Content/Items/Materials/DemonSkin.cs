using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Materials
{
    public class DemonSkin : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Orange;
            Item.material = true;
        }
    }
}