using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;

namespace TritonsHydrants.Content.Items.Weapons.Tridents
{
    public class IronTrident : ModItem
    {
        public override void SetDefaults()
        {
            // Item settings
            Item.damage = 6;
            Item.knockBack = 6.5f;
            Item.mana = 5;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 10);
            Item.shoot = ModContent.ProjectileType<IronTridentProj>();
            Item.useAnimation = 31;
            Item.useTime = 31;

            // Texture settings
            Item.width = 44;
            Item.height = 44;
            Item.scale = 1f;

            // Default Stats
            Item.shootSpeed = 9f;
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
    public class IronTridentProj : TridentBase
    {
        protected override float HoldoutRangeMax => 120f;
        public override void SetDefaults()
        {

            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.aiStyle = ProjAIStyleID.Spear;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
            Projectile.alpha = 0;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
    }
}