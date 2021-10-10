using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Buffs
{
    public class BattleCry : ModBuff
    {
        public override void SetStaticDefaults(){
            
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Battle Cry");
            Description.SetDefault("Enemies are more likely to target you");
        }

        public override void Update(Player player, ref int buffIndex){

            player.aggro += 100;
        }
    }
}