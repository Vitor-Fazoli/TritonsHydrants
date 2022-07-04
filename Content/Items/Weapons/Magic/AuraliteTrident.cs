using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GearonArsenal;

namespace GearonArsenal.Content.Items.Weapons.Magic {
	public class AuraliteTrident : ModItem {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Auralite Trident");
		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useAnimation = 18;
			Item.useTime = 30;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true; 
			Item.noUseGraphic = true; 
			Item.autoReuse = true; 
			Item.UseSound = SoundID.Item1;
			Item.mana = 10;
			Item.shoot = ModContent.ProjectileType<Projectiles.TridentProjectile>();
		}
        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<AuraliteTrident>(4)
				.AddIngredient(ItemID.DemoniteBar, 5)
				.AddTile(TileID.Anvils)
				.Register();
        }
        public override bool CanUseItem(Player player) {
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}
}
