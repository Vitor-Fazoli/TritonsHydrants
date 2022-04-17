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
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.width = 400;
            Projectile.height = 200;


            AIType = ProjectileID.Arkhalis;
        }
        public override bool PreDraw(ref Color lightColor) {
            lightColor = new(default, default, default, 100);

            return base.PreDraw(ref lightColor);
        }
    }
}
