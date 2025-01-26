using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Common.Players;
namespace TritonsHydrants.Content.Buffs
{
    /// <summary>
    /// 
    /// </summary>
    public class Refueled : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<HydrantPlayer>().isRefueled = true;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.2f; // 10% Attack speed
        }
    }
}