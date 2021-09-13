using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Buffs
{
    public class Marked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense /= 2;

            effectBuff(npc);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 2;

            effectBuff(player);
        }
        public void effectBuff(Entity target)
        {
            int num1 = Dust.NewDust(target.position, target.width, target.height, DustID.Blood);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }
    }
}