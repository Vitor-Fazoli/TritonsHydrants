using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GreatswordsMod.Abstract;
using Microsoft.Xna.Framework;

namespace GreatswordsMod.Item
{
	public class CopperGreatsword : ItemGreatsword
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Copper Greatsword");
			Tooltip.SetDefault("holding the attack it will come out stronger");
		}

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
			dmg = 6;
			knk = 1;
			cooldown = 30;
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
			spdFrame = 5;
		}
	}
	public class CopperDash : Dash
    {

    }
}