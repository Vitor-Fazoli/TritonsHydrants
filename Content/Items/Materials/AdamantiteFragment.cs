using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Materials
{
    public class AdamantiteFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Fragment");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.material = true;
        }
    }
}
