using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Buffs
{
    public class WaterAffinity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.accFlipper = true;
            player.waterWalk = true;

            if (player.wet)
            {
                player.moveSpeed += 0.20f;
            }
        }
    }
}