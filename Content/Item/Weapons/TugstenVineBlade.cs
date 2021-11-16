using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GearonArsenalMod.Abstract;
using Microsoft.Xna.Framework;

namespace GearonArsenalMod.Content.Item.Weapons
{
	public class TugstenVineBlade : ItemGreatsword
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Tugsten Vineblade");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{

			//properties - Default
			Item.damage = ModContent.GetInstance<TugstenVineBladeP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = ModContent.GetInstance<TugstenVineBladeP>().GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = greatsword;
			Item.channel = true;
			Item.crit = -4;

			//properties - Item
			greatsword = ModContent.ProjectileType<TugstenVineBladeP>();
			gStats = ModContent.GetInstance<TugstenVineBladeP>();
			slash = ModContent.ProjectileType<VineSlash>();
		}
		public override void AddRecipes()
		{

			CreateRecipe()
			.AddIngredient(ItemID.CopperBar, 9)
			.AddIngredient(ItemID.CopperShortsword, 3)
			.AddIngredient(ItemID.SpookyClock, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class TugstenVineBladeP : Greatsword
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
			gDamage = 11;
			cooldown = 73;
			aggro = 10;
			defense = 3;
			slash = ModContent.ProjectileType<VineSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class VineSlash : Slash
	{
        public override global::System.String Texture => ("GearonArsenalMod/Abstract/VineSlash");
        public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("VineSlash Slash");
			Main.projFrames[Projectile.type] = frames;
		}
	}
}