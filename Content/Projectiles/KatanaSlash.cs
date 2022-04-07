using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace GearonArsenal.Content.Projectiles {
    internal class KatanaSlash : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Katana Slash");
            Main.projFrames[Projectile.type] = 11;
        }
        public override void SetDefaults() {
            Projectile.width = 400;
            Projectile.height = 200;
            Projectile.CloneDefaults(ProjectileID.Arkhalis);

            AIType = ProjectileID.Arkhalis;
        }
        public override bool PreDraw(ref Color lightColor) {
            lightColor = new(default, default, default, 100);

            return base.PreDraw(ref lightColor);
        }
    }
}
