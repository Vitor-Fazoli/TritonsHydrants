using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace GearonArsenal.Common {
    /// <summary>
    /// This class below make possible a new form to play with a magic, you turn a blood mage and use your vitality
    /// </summary>
    public class Witch : ModPlayer{
        public bool witchMask = false;
        public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
            if (witchMask) {
                reduce -= 0.5f;
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed) {
            if (witchMask) 
                Player.statLife -= manaConsumed;

            Player.statMana = Player.statLifeMax2;
        }
    }
    /// <summary>
    /// Part of a big change, your items change when you are wearing Witch Mask
    /// </summary>
    public class WitchSystem : GlobalItem {
        public override bool CanUseItem(Item item, Player player) {
            //making a player dont kill yourself with a witch mask
            if (item.DamageType == DamageClass.Magic && player.GetModPlayer<Witch>().witchMask == true) {
                if (player.statLife <= item.mana) {
                    return false;
                }
            }
            return base.CanUseItem(item, player);
        }
    }
}
