using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace VoidArsenal.Content.Items.Weapons.Ranged.Gun
{
    public class Raptor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raptor");
            Tooltip.SetDefault("A powerful firerate");
        }
        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 30;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.reuseDelay = 4;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item31;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 3;
            Item.knockBack = 2f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 0f);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ProjectileID.BulletHighVelocity;
        }
    }
}