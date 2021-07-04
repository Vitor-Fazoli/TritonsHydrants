using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Weapon;
using GreatswordsMod.Attack;
using Terraria;

namespace GreatswordsMod.Item
{
	public class DemoniteGreatsword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Demonite Greatsword");
			Tooltip.SetDefault("holding the attack it will come out stronger");
		}

		public override void SetDefaults() 
		{
			item.damage = 16;
			item.melee = true;
			item.useTime = 15;
			item.useAnimation = 15;
			item.reuseDelay = 2;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 15;
			item.value = 250000;
			item.rare = ItemRarityID.White;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<DemoniteGreatswordP>();
			item.channel = true;
		}
        public override bool CanUseItem(Player player)
        {
			return player.ownedProjectileCounts[item.shoot] + 
				player.ownedProjectileCounts[ModContent.ProjectileType<DemoniteGreatswordP>()] + 
				player.ownedProjectileCounts[ModContent.ProjectileType<DemoniteSlash>()] < 1;
		}
	}
}