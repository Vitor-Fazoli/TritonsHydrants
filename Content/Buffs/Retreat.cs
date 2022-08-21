using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Buffs
{
    public class Retreat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RETREAT");
            Description.SetDefault("Supplies will give you a battle sigh to escape");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.10f;
        }
    }
}
