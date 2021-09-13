using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using GearonArsenalMod.Item;
using System.Collections.Generic;

namespace GearonArsenalMod.Abstract
{
    public abstract class ItemGreatsword : ModItem
    {
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.channel = true;
			Item.crit = 0; //Dont touch 
		}
        public override void HoldItem(Player player)
        {
			player.endurance += 0.25f;
			player.aggro += 10;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			position.Y = player.position.Y - 62;
		}
	}
}