using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GearonArsenalMod.Abstract;
using Microsoft.Xna.Framework;

namespace GearonArsenalMod.Content.Item.Weapons
{
	public class CopperGreatsword : ItemGreatsword{
		public override void SetStaticDefaults(){

			DisplayName.SetDefault("Copper Greatsword");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults(){

			//properties - Default
			Item.damage = ModContent.GetInstance<CopperGreatswordP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = ModContent.GetInstance<CopperGreatswordP>().GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = greatsword;
			Item.channel = true;
			Item.crit = -4;

			//properties - Item
			greatsword = ModContent.ProjectileType<CopperGreatswordP>();
			gStats = ModContent.GetInstance<CopperGreatswordP>();
			slash = ModContent.ProjectileType<CopperSlash>();
		}
        public override void AddRecipes(){

			CreateRecipe()
			.AddIngredient(ItemID.CopperBar, 9)
			.AddIngredient(ItemID.CopperShortsword, 3)
			.AddIngredient(ItemID.SpookyClock, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class CopperGreatswordP : Greatsword{

		public override void SetDefaults(){

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
			gDamage = 9;
			cooldown = 75;
			aggro = 10;
			defense = 3;
			slash = ModContent.ProjectileType<CopperSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class CopperSlash : Slash{

		public override void SetStaticDefaults(){

			DisplayName.SetDefault("Cooper Slash");
			Main.projFrames[Projectile.type] = frames;
		}
		public override bool PreDraw(ref Color lightColor){

			lightColor = new(228, 150, 105);
			return base.PreDraw(ref lightColor);
		}
	}
}