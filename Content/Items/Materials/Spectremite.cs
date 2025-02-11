using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Materials
{
    public class Spectremite : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Purple;
        }
    }
}