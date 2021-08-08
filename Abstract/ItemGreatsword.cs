using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using GreatswordsMod.Item;

namespace GreatswordsMod.Abstract
{
    public abstract class ItemGreatsword : ModItem
    {
		public override void SetDefaults()
		{
			WeaponStyleSecondary(ModContent.ProjectileType<CopperDash>());		
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public void WeaponStylePrimary(int dmg, int knk, int crit, int weapon)
        {
			Item.damage = dmg;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = knk;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = weapon;
			Item.channel = true;
			Item.crit = 6 + crit;
		}

		public void WeaponStyleSecondary(int weapon)
        {
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.damage = 50;
			Item.shoot = weapon;
			Item.width = 20;
			Item.height = 20;
			Item.noUseGraphic = true;
			Item.autoReuse = false;
			Item.DamageType = DamageClass.Melee;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if(player.altFunctionUse != 2)
            {
				position.Y = player.position.Y - 62;
			}
		}
	}
}