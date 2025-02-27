using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Buffs
{
    public class HighTide : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}