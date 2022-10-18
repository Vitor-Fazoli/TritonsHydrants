using Terraria;
using Terraria.ModLoader;


namespace DevilsWarehouse.Content.Projectiles
{
    public class MaidenProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maiden");
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.velocity.Y = Projectile.velocity.Y + 0.5f;
        }
    }
}
