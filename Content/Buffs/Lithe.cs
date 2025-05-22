using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Common.Players;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Buffs;

public class Lithe : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        HydrantPlayer hydrantP = player.GetModPlayer<HydrantPlayer>();

        player.GetDamage(DamageClass.Ranged) += TritonsHelper.Percentage(15);
        hydrantP.isLithe = true;
    }
}