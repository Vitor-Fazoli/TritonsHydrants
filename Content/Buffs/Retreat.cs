using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class Retreat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RETREAT");
            Description.SetDefault("Supplies was give you a battle sigh to escape");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.10f;
        }
    }
}
