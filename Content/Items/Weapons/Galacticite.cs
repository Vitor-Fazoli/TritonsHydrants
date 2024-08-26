using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using NeptunesTreasure.Content.Projectiles;
using Terraria.Audio;

namespace NeptunesTreasure.Content.Items.Weapons
{
    public class Galacticite : ModItem
    {
        public override void SetDefaults()
        {
            // Item settings
            Item.damage = 200;
            Item.knockBack = 4.5f;
            Item.mana = 20;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(gold: 23);
            Item.shoot = ModContent.ProjectileType<GalacticiteProj>();
            Item.useAnimation = 11;
            Item.useTime = 11;

            // Texture settings
            Item.width = 44;
            Item.height = 44;
            Item.scale = 1f;

            // Default Stats
            Item.shootSpeed = 11f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item71;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 7)
                .AddIngredient(ItemID.Wood, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
    }
}