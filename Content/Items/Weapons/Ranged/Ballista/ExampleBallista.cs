using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using VoidArsenal.Content.Projectiles;

namespace VoidArsenal.Content.Items.Weapons.Ranged.Ballista
{
	public class ExampleBallista : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Ballista");
			Tooltip.SetDefault(@"A slightly different laser-firing Prism
Ignores NPC immunity frames and fires 10 beams at once instead of 6.");
		}

		public override void SetDefaults()
		{
			// Start by using CloneDefaults to clone all the basic Item properties from the vanilla Last Prism.
			// For example, this copies sprite size, use style, sell price, and the Item being a magic weapon.
			Item.CloneDefaults(ItemID.LastPrism);
			Item.mana = 4;
			Item.damage = 42;
			Item.shoot = ModContent.ProjectileType<ExampleBallistaHoldout>();
			Item.shootSpeed = 30f;

		}

		// Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<ExampleBallistaHoldout>()] <= 0;
	}
}