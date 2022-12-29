using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs.Vampire
{
    public class BatFormBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bat Form");
            Description.SetDefault("Leather seats, 4 cup holders");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Mounts.BatForm>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
