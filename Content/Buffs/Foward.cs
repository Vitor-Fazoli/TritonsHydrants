using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    internal class Foward : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FOWARD");
            Description.SetDefault("Supplies will give you strenght to fight");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
    }
}
