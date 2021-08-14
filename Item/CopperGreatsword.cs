using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GreatswordsMod.Abstract;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GreatswordsMod.Item
{
	public class CopperGreatsword : ItemGreatsword
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Copper Greatsword");
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
			Item.crit = -4;
		}
		public override bool CanUseItem(Player player)
		{
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<CopperGreatswordP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<CopperSlash>()] < 1;
		}
        public override void AddRecipes()
        {
			CreateRecipe()
			.AddIngredient(ItemID.CopperBar, 9)
			.AddIngredient(ItemID.CopperShortsword)
			.AddTile(TileID.Anvils)
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
			dmg = 10;
			cooldown = 90;
			proj = ModContent.ProjectileType<CopperSlash>();
			wEffect = DustID.Cloud;
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
			Projectile.DamageType = DamageClass.Melee;

			//properties - Slash
			frames = 5;
			spdFrame = 4;
		}
	}
}