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
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<SpinSword>();
            Item.shootSpeed = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
        }
    }
}