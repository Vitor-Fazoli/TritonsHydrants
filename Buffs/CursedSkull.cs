using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Buffs
{
    public class CursedSkull : ModBuff
    {
        public override void SetStaticDefaults(){

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Cursed Skulls");
            Description.SetDefault("the bones will try to protect you");
        }
    }
}