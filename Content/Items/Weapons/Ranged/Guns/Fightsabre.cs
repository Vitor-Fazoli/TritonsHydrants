using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using DevilsWarehouse.Content.Projectiles;

namespace DevilsWarehouse.Content.Items.Weapons.Ranged.Guns
{
    public class Fightsabre : ModItem
    {
        public const int BULLETMAX = 25;
        public const float RELOADTIME = 1.5f;
        public int bullets = 25;
        public int time;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 24;

            Item.damage = 3;
            Item.crit = 1;
            Item.knockBack = 1;
            Item.autoReuse = true;

            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.shootSpeed = 16f;

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3,1);
        }
        public override void OnSpawn(IEntitySource source)
        {
            bullets = BULLETMAX;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            bullets -= 1;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 90;
                Item.useAnimation = 90;
                Item.noUseGraphic = true;
                Item.shoot = ModContent.ProjectileType<FightsabreProj>();
                Item.useAmmo = AmmoID.None;
                bullets = 25;
            }
            else
            {
                if (bullets > 0)
                {
                    Item.width = 70;
                    Item.height = 24;

                    Item.damage = 3;
                    Item.crit = 1;
                    Item.knockBack = 1;
                    Item.autoReuse = true;

                    Item.useAnimation = 6;
                    Item.useTime = 6;
                    Item.shootSpeed = 16f;

                    Item.useAmmo = AmmoID.Bullet;
                    Item.shoot = ProjectileID.Bullet;
                    Item.useStyle = ItemUseStyleID.Shoot;

                    Item.noUseGraphic = false;
                }
                else
                {
                    Item.noUseGraphic = true;
                    return false;
                }
            }
            return base.CanUseItem(player);
        }
    }
}
