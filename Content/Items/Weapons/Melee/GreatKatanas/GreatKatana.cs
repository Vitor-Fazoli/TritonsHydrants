using GearonArsenal.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Weapons.Melee.GreatKatanas {
    public class GreatKatana : ModItem {
        public override void SetDefaults() {

            Item.CloneDefaults(ItemID.Arkhalis);

            Item.shoot = ModContent.ProjectileType<KatanaSlash>();
        }
    }
}