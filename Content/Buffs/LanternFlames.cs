using GearonArsenalMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.Buffs
{
    public class LanternFlames : ModBuff
    {
        public  override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Lantern Flames");
            Description.SetDefault("Increased Fury Counter");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GreatswordPlayer>().lantern = true;
        }
    }
}
