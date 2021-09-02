using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GreatswordsMod.Abstract;
using Microsoft.Xna.Framework;

namespace GreatswordsMod.Item
{
	public class TugstenGreatsword : ItemGreatsword
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Copper Greatsword");
			DisplayName.AddTranslation(8, "Espada Grande de Cobre");

			Tooltip.SetDefault("Hold attack to greater damage\nHolding weapon to increase your resistance");
			Tooltip.AddTranslation(8, "Segure o ataque para maior dano\nEnquanto estiver com a espada selecionada ganha resistencia");
		}
		public override void SetDefaults()
        {
			Item.damage = ModContent.GetInstance<TugstenGreatswordP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = TugstenGreatswordP.GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<TugstenGreatswordP>();
			Item.channel = true;
			Item.crit = -4;
		}
		public override bool CanUseItem(Player player)
		{
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<TugstenGreatswordP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<TugstenSlash>()] < 1;
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
	public class TugstenGreatswordP : Greatsword
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
			proj = ModContent.ProjectileType<TugstenSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class TugstenSlash : Slash
	{
		public override string Texture => ("GreatswordsMod/Abstract/Slash");
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tugsten Slash");
			DisplayName.AddTranslation(8, "Corte de Tugstenio");
			Main.projFrames[Projectile.type] = frames;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			lightColor = new(102, 172, 126);
			return base.PreDraw(ref lightColor);
		}
	}
}