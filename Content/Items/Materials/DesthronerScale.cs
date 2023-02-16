using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Materials
{
    public class DesthronerScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
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
