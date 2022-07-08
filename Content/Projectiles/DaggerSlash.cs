using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace VoidArsenal.Content.Projectiles {
    internal class DaggerSlash : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Dagger Slash");
            Main.projFrames[Projectile.type] = 28;
        }
        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.width = 64;
            Projectile.height = 68;


            AIType = ProjectileID.Arkhalis;
        }
        public override bool PreDraw(ref Color lightColor) {
            lightColor = new(255, 255, 255, 75);

            return base.PreDraw(ref lightColor);
        }
    }
}
