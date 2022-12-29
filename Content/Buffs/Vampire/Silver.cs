using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs.Vampire
{
    public class Silver : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silver Pain");
            Description.SetDefault("");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = 0;
        }
    }
}
