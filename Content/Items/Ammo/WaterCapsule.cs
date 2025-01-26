using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Ammo
{
    public class WaterCapsule : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;

            Item.maxStack = 99;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 0, 20);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<Projectiles.AquaBurst>();
            Item.ammo = Item.type;
        }

        public override void AddRecipes()
        {
            CreateRecipe(30)
                .AddIngredient(ItemID.BottledWater, 1)
                .AddRecipeGroup(RecipeGroupID.IronBar, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}