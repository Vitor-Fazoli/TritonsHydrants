using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace MagicTridents.Content.Items.Weapons
{
    public class Galacticite : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 230;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.useAnimation = 8;
            Item.useTime = 13;
            Item.shootSpeed = 3.0f;
            Item.knockBack = 10.5f;
            Item.width = 44;
            Item.height = 44;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 10);

            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.mana = 10;
            Item.shoot = ModContent.ProjectileType<Projectiles.GalacticiteProj>();
        }

        public override void AddRecipes()
        {
            //CreateRecipe()
            //    .AddIngredient<AuraliteOre>(10)
            //    .AddTile(TileID.Anvils)
            //    .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}