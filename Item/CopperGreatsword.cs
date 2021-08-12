using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GreatswordsMod.Abstract;
using Microsoft.Xna.Framework;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99

namespace GreatswordsMod.Item
{
	public class CopperGreatsword : ItemGreatsword
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Copper Greatsword");
			Tooltip.SetDefault("holding the attack it will come out stronger");
		}
<<<<<<< HEAD
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tooltip = new(Mod, "GreatswordsMod: Copper Greatsword", $"Hold for greater damage!") { overrideColor = Color.Red };
			tooltips.Add(tooltip);
		}
		public override void SetDefaults()
        {
			Item.damage = ModContent.GetInstance<CopperGreatswordP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = CopperGreatswordP.GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Item.CopperGreatswordP>();
			Item.channel = true;
			Item.crit = CopperGreatswordP.GetCrit();
		}
        public override bool CanUseItem(Player player)
		{
=======

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				WeaponStyleSecondary(ModContent.ProjectileType<CopperDash>());
			}
			else
			{
				WeaponStylePrimary(
					ModContent.GetInstance<CopperGreatswordP>().GetDmg(),
					ModContent.GetInstance<CopperGreatswordP>().GetKnk(),
					ModContent.GetInstance<CopperGreatswordP>().GetCrit(),
					ModContent.ProjectileType<CopperGreatswordP>()
					);
			}
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<CopperGreatswordP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<CopperSlash>()] < 1;
		}
        public override void AddRecipes()
        {
			CreateRecipe()
			.AddIngredient(ItemID.DirtBlock, 5)
			.Register();
		}
	}
	public class CopperGreatswordP : Greatsword
	{
		public override void SetDefaults()
		{
			//properties - Default
			Projectile.width = 0;
			Projectile.height = 0;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 124;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.ownerHitCheck = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.extraUpdates = 1;

			//properties - Greatsword
<<<<<<< HEAD
			dmg = 10;
			cooldown = 60;
=======
			dmg = 6;
			knk = 1;
			cooldown = 30;
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99
			proj = ModContent.ProjectileType<CopperSlash>();
			wEffect = 20;
		}
		public override void AI()
		{
			base.AI();
		}
	}
	public class CopperSlash : Slash
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cooper Slash");
			Main.projFrames[Projectile.type] = frames;
		}
		public override void SetDefaults()
		{
			//properties - Default
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;

			//properties - Slash
			frames = 5;
<<<<<<< HEAD
			spdFrame = 4;
		}
	}
=======
			spdFrame = 5;
		}
	}
	public class CopperDash : Dash
    {

    }
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99
}