using DevilsWarehouse.Common;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class LampionFlames : ModBuff
    {
        public bool equip;
        public  override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Lampiom Flames");
            Description.SetDefault("Increased Fury Counter");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();
            modPlayer.Lampion = true;
            if(!equip)
                modPlayer.slayerMax += 2;
            equip = true;
        }
    }
}
