using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TritonsHydrants.Content.Buffs;

namespace TritonsHydrants.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HoseBase : ModItem
    {
        protected const int MIN_MANA_COST = 10;
        protected const int MIN_USE_TIME_ANIMATION = 30;

        protected virtual int ManaCost { get; set; }
        protected virtual int BurstDamage { get; set; }
        protected virtual int BurstKnockback { get; set; }
        protected virtual int UseTimeAnimation { get; set; }

        public override void HoldItem(Player player)
        {
            if (player.HeldItem.backSlot > 0)
            {
                player.back = player.HeldItem.backSlot;
                player.cBack = 0;
            }
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return player.GetModPlayer<HydroCanisterBoosterPlayer>().IsConsumed;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltip in tooltips)
            {
                if (tooltip.Name != "ItemName")
                {
                    tooltip.Hide();
                }

                //TODO: Colocar uma descrição que recebe  todas as especificações do item como Tamanho da area do hidrante dano do burst e etc
            }
        }
    }
}
