using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Materials
{
    public class SmoothScales : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smooth Scales");
            Tooltip.SetDefault("So soft it makes you want to sleep");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.material = true;
        }
    }
}
