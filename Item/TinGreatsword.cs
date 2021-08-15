using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatswordsMod.Abstract;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GreatswordsMod.Item
{
    class TinGreatsword : ItemGreatsword
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tin Greatsword");
			DisplayName.AddTranslation(8, "Espada Grande de Estanho");

			Tooltip.SetDefault("Hold attack to greater damage\nHolding weapon to increase your resistance");
			Tooltip.AddTranslation(8, "Segure o ataque para maior dano\nEnquanto estiver com a espada selecionada ganha resistencia");
		}
		public override void SetDefaults()
		{
			Item.damage = ModContent.GetInstance<TinGreatswordP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = TinGreatswordP.GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<TinGreatswordP>();
			Item.channel = true;
			Item.crit = -4;
		}
		public override bool CanUseItem(Player player)
		{
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<TinGreatswordP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<TinSlash>()] < 1;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TinBar, 9)
			.AddIngredient(ItemID.TinShortsword)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
	public class TinGreatswordP : Greatsword
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
			dmg = 11;
			cooldown = 90;
			proj = ModContent.ProjectileType<TinSlash>();
			wEffect = DustID.Cloud;
		}
	}
	public class TinSlash : Slash
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tin Slash");
			DisplayName.AddTranslation(8, "Corte de Estanho");
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
