using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    internal class Standing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("STANDING");
            Description.SetDefault("Supplies was give you resilience to withstand the battle");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 5;
        }
    }
}
