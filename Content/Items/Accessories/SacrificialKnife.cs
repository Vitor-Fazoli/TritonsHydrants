using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Accessories
{
    public class SacrificialKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Increase your Critical Chance but decreased your life max");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
        }
        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 11;

            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 60);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 0.10f;
            player.statLifeMax -= 30;
        }
    }
}
