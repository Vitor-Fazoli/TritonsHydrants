using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Materials
{
    public class AdamantiteFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Fragment");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.material = true;
        }
    }
}
