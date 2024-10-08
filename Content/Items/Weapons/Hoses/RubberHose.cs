using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;

namespace TritonsHydrants.Content.Items.Weapons.Hoses
{
    public class RubberHose : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.knockBack = 6.75f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 32;
            Item.crit = 7;
            Item.scale = 1.1f;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<HoseBase>();
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.channel = true;
            Item.noMelee = true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 3)
                .AddIngredient(ItemID.Wood, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}