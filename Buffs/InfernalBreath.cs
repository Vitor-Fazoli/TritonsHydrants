using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Buffs
{
    public class InfernalBreath : ModBuff
    {
        public override void SetStaticDefaults(){

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Infernal Breath");
            Description.SetDefault("");
        }

        public override void Update(Player player, ref int buffIndex){
            
            player.thorns += 10;
        }
    }
}