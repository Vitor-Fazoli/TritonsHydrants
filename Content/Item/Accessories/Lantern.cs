using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GearonArsenalMod.Content.Buffs;
using GearonArsenalMod.Common.Players;

namespace GearonArsenalMod.Content.Item.Accessories
{
    public class Lantern : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lantern");
            Tooltip.SetDefault("Increased Fury Counter\n[only for greatswords]");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<LanternFlames>(), 20);
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<GreatswordPlayer>().balm)
                return false;


            return true;
        }
    }
}