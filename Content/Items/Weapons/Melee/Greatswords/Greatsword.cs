using GearonArsenal.Common;
using GearonArsenal.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Weapons.Melee.Greatswords {
    public class Greatsword : ModItem {
        public override void SetDefaults() {

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 10;
            Item.crit = 6;
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<SpinSword>();
            Item.shootSpeed = 6;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int bloodPoint = 0;

            if (bloodPoint >= 3)
            {
                target.life = 0;
                bloodPoint = 0;
            }

            bloodPoint++;

        }
    }
}