using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Content.Buffs;
using TritonsHydrants.Content.Items.Ammo;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Content.Items.Weapons.Hoses
{
    public class CopperHose : ModItem
    {
		public override void SetDefaults() {
			Item.width = 42;
			Item.height = 30;
            Item.autoReuse = true;
            Item.damage = 0;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.White;
            Item.mana = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 5f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Hydrant>();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse is 2)
			{
				Item.mana = player.statMana / 3;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shootSpeed = 3f;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.noUseGraphic = true;
				Item.shoot = ModContent.ProjectileType<Hydrant>();
				Item.useAmmo = 0;
				Item.noMelee = true;
				Item.buffType = ModContent.BuffType<HydrantBuff>();
			}
			else
			{
				Item.mana = 0;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noMelee = true;
				Item.shootSpeed = 10f;
				Item.useAnimation = 90;
				Item.useTime = 90;
				Item.damage = 45;
				Item.noUseGraphic = false;
				Item.shoot = ModContent.ProjectileType<AquaBurst>();
				Item.useAmmo = ModContent.ItemType<WaterCapsule>();
				Item.DamageType = DamageClass.Ranged;
				Item.buffType = 0;
			}
			return base.CanUseItem(player);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return player.GetModPlayer<HydroCanisterBoosterPlayer>().IsConsumed;
		}
		public override Vector2? HoldoutOffset() {
        	return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
	        if (player.altFunctionUse is not 2)
		        return true;
	        
	        player.AddBuff(Item.buffType, 2);
	        
	        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, 0, 0, Main.myPlayer);
	        projectile.originalDamage = 0;

	        return false;
        }
    }
}