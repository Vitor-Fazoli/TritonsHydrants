using DevilsWarehouse.Common.Systems.JutsuSystem;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common.Abstract
{
    public abstract class Jutsu : ModItem
    {
        protected int chakraCost = 4;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Chakra Cost", $"Uses {chakraCost} chakra"));
        }
        public override bool CanUseItem(Player player)
        {
            var p = player.GetModPlayer<JutsuPlayer>();

            if (p.chakraCurrent >= chakraCost)
            {
                p.chakraCurrent -= chakraCost;
                return true;
            }

            return false;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            var p = player.GetModPlayer<JutsuPlayer>();

            crit = p.chakraMax2 / 20;
        }
    }
}