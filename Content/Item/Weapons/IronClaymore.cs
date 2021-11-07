using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GearonArsenalMod.Abstract;
using Microsoft.Xna.Framework;

namespace GearonArsenalMod.Content.Item.Weapons
{
	public class IronClaymore : ItemGreatsword{

		public override void SetStaticDefaults(){

			DisplayName.SetDefault("Iron Claymore");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults(){

			//properties - Default
			Item.damage = ModContent.GetInstance<IronClaymoreP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = ModContent.GetInstance<IronClaymoreP>().GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = greatsword;
			Item.channel = true;
			Item.crit = -4;

			//properties - Item
			greatsword = ModContent.ProjectileType<IronClaymoreP>();
			gStats = ModContent.GetInstance<IronClaymoreP>();
			slash = ModContent.ProjectileType<IronSlash>();
		}
		public override void AddRecipes(){

			CreateRecipe()
			.AddIngredient(ItemID.IronBar, 9)
			.AddIngredient(ItemID.IronShortsword)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class IronClaymoreP : Greatsword{

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
			gDamage = 13;
			cooldown = 75;
			aggro = 10;
			defense = 3;
			slash = ModContent.ProjectileType<IronSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class IronSlash : Slash{

		public override void SetStaticDefaults(){

			DisplayName.SetDefault("Iron Slash");
			Main.projFrames[Projectile.type] = frames;
		}
		public override bool PreDraw(ref Color lightColor){

			lightColor = new(228, 150, 105);
			return base.PreDraw(ref lightColor);
		}
	}
}