using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
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
	    public override void SetStaticDefaults() {
		    //AmmoID.Sets.SpecificLauncherAmmoProjectileMatches.Add(Type, new Dictionary<int, int>
		    //{
			//    { ItemID.RocketIII, ProjectileID.Meowmere },
		    //});
	    }
		public override void SetDefaults() {
			Item.width = 42;
			Item.height = 30;
            Item.autoReuse = true;
            Item.damage = 0;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.White;
            Item.mana = 20;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.shootSpeed = 5f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 0;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Canister>();
		}

		public override bool AltFunctionUse(Player player)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<Canister>()] <= player.maxMinions; 
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse is 2)
			{
				Item.mana = player.statMana / 3;
				Item.useStyle = ItemUseStyleID.Thrust;
				Item.shootSpeed = 3f;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.noUseGraphic = true;
				Item.shoot = ModContent.ProjectileType<Canister>();
				Item.useAmmo = 0;
			}
			else
			{
				Item.mana = 0;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noMelee = true;
				Item.shootSpeed = 10f;
				Item.useAnimation = 90;
				Item.useTime = 90;
				Item.damage = 12;
				Item.noUseGraphic = false;
				Item.shoot = ModContent.ProjectileType<AquaBurst>();
				Item.useAmmo = ModContent.ItemType<WaterCapsule>();
			}
			return base.CanUseItem(player);
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return player.GetModPlayer<HydroCanisterBoosterPlayer>().IsConsumed;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-25f, -5f);
		}
    }
}