using GearonArsenalMod.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GearonArsenalMod.Common.Players;

namespace GearonArsenalMod.Content.Item.Accessories {
    public class Balm : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Decreased Fury Counter but increased buff time\n[only for greatswords]");
        }
        public override void SetDefaults() {
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.material = true;
            Item.value = 20 * 99 * 99;
        }
        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.AddBuff(ModContent.BuffType<BalmPower>(), 20);
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded) {
            if (player.GetModPlayer<GreatswordPlayer>().lantern)
                return false;


            return true;
        }
    }
}
