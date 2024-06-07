using MagicTridents.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MagicTridents.Content.Items.Weapons
{
    /// <summary>
    /// 
    /// </summary>
    public class IronTrident : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 31;
            Item.useTime = 31;
            Item.shootSpeed = 4f;
            Item.knockBack = 6.5f;
            Item.width = 44;
            Item.height = 44;
            Item.scale = 1f;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 10);

            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item71;
            Item.mana = 5;
            Item.shoot = ModContent.ProjectileType<IronTridentProj>();
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