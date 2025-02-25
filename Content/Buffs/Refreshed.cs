using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Common.Players;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Buffs
{
    /// <summary>
    /// Heal the player when hit by a NPC or projectile if is on Hydrant area and receive 10% attack speed
    /// </summary>
    public class Refreshed : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            var hydrantP = player.GetModPlayer<HydrantPlayer>();

            player.GetKnockback(DamageClass.Ranged) += Helper.Percentage(10);
            hydrantP.isRefreshed = true;
        }
    }
}