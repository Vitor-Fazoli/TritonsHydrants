using GearonArsenalMod.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.Item.Accessories
{
    public class Lampion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lampion");
            Tooltip.SetDefault("Increased Fury Counter\n[only for greatswords]");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();
            modPlayer.lampion = true;
        }
    }
}