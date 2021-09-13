using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GearonArsenalMod.Abstract;
using Microsoft.Xna.Framework;

namespace GearonArsenalMod.Item
{
	public class PlatinumGreatsword : ItemGreatsword
	{
        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Platinum Greatsword");
			DisplayName.AddTranslation(8, "Espada Grande de Platina");

			Tooltip.SetDefault("Hold attack to greater damage\nHolding weapon to increase your resistance");
			Tooltip.AddTranslation(8, "Segure o ataque para maior dano\nEnquanto estiver com a espada selecionada ganha resistencia");
		}
		public override void SetDefaults()
        {
			Item.damage = ModContent.GetInstance<PlatinumGreatswordP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = PlatinumGreatswordP.GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<PlatinumGreatswordP>();
			Item.channel = true;
			Item.crit = -4;
		}
		public override bool CanUseItem(Player player)
		{
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<PlatinumGreatswordP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<PlatinumSlash>()] < 1;
		}
        public override void AddRecipes()
        {
			CreateRecipe()
			.AddIngredient(ItemID.PlatinumBar, 9)
			.AddIngredient(ItemID.PlatinumShortsword)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class PlatinumGreatswordP : Greatsword
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
			cooldown = 100;
			proj = ModContent.ProjectileType<CopperSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class PlatinumSlash : Slash
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Platinum Slash");
			DisplayName.AddTranslation(8, "Corte de Platina");
			Main.projFrames[Projectile.type] = frames;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			lightColor = new(235, 167, 136);
			return base.PreDraw(ref lightColor);
		}
	}
}