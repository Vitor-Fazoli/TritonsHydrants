using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Common.Players;
using TritonsHydrants.Utils;
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
            player.GetAttackSpeed(DamageClass.Ranged) += Helper.Percentage(10);
        }
    }
}