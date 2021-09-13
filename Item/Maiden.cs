using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using GearonArsenalMod.Abstract;
using GearonArsenalMod.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using GearonArsenalMod.Buffs;

namespace GearonArsenalMod.Item
{
	public class Maiden : ItemGreatsword
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maiden");
			DisplayName.AddTranslation(8, "Donzela");

			Tooltip.SetDefault("Hold attack to greater damage\nHolding weapon to increase your resistance");
			Tooltip.AddTranslation(8, "Segure o ataque para maior dano\nEnquanto estiver com a espada selecionada ganha resistencia");
		}
		public override void SetDefaults()
		{
			Item.damage = ModContent.GetInstance<MaidenP>().GetDmg();
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = MaidenP.GetKnk();
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<MaidenP>();
			Item.channel = true;
			Item.crit = -4;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
            ModPlayer modPlayer = player.GetModPlayer<ModPlayer>();
			if (player.altFunctionUse == 2)
			{
				if(modPlayer.slayerPower >= 3 && player.ownedProjectileCounts[ModContent.ProjectileType<BoneProtector>()] <= 0)
                {
					Item.useStyle = ItemUseStyleID.HoldUp;
					Item.useTime = 20;
					Item.useAnimation = 20;
					Item.damage = 50;
					Item.shoot = ModContent.ProjectileType<BoneProtector>();
					modPlayer.slayerPower = 0;
					player.AddBuff(ModContent.BuffType<CursedSkull>(),1800);
				}
                else
                {
					return false;
                }
			}
			else
			{
				Item.damage = ModContent.GetInstance<MaidenP>().GetDmg();
				Item.DamageType = DamageClass.Melee;
				Item.useTime = 50;
				Item.useAnimation = 50;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.knockBack = MaidenP.GetKnk();
				Item.noUseGraphic = true;
				Item.noMelee = true;
				Item.shoot = ModContent.ProjectileType<MaidenP>();
				Item.channel = true;
				Item.crit = -4;
			}
			
			return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
				player.ownedProjectileCounts[ModContent.ProjectileType<MaidenP>()] +
				player.ownedProjectileCounts[ModContent.ProjectileType<MaidenSlash>()] < 1;
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
	public class MaidenP : Greatsword
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
			dmg = 37;
			cooldown = 85;
			proj = ModContent.ProjectileType<MaidenSlash>();
			wEffect = 16;
			color = new(58, 50, 85);
		}
	}
	public class MaidenSlash : Slash
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maiden Slash");
			DisplayName.AddTranslation(8, "Corte da Donzela");
			Main.projFrames[Projectile.type] = frames;
		}
        public override void SetDefaults()
        {
			Projectile.width = 400;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Melee;
		}

        public override bool PreDraw(ref Color lightColor)
        {
			lightColor = new(58,50,85);
			return base.PreDraw(ref lightColor);
        }
    }
}