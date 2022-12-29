using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs.Vampire
{
    public class Vampirism : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampirism");
            Description.SetDefault("you are a vampire");
        }
        public override bool RightClick(int buffIndex)
        {
            return false;
        }
    }
}
