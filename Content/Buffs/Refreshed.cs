using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Common.Players;

namespace TritonsHydrants.Content.Buffs
{
    /// <summary>
    /// Heal the player when hit by a NPC or projectile if is on Hydrant area and receive 10% attack speed
    /// </summary>
    public class Refreshed : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Ranged) += 0.1f; //10% attack speed
            player.GetModPlayer<HydrantPlayer>().isRefreshed = true;
        }
    }
}