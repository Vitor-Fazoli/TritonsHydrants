using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Buffs
{
    internal class Standing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("STANDING");
            Description.SetDefault("Supplies will give you resilience to withstand the battle");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 5;
        }
    }
}
