using GearonArsenalMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.Buffs {
    public class BalmPower : ModBuff {
        public override void SetStaticDefaults() {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Balm Power");
            Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<GreatswordPlayer>().balm = true;
        }
    }
}
