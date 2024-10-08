using Terraria.ModLoader;

namespace TritonsHydrants.Common
{
    public class GallonBase : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
            Projectile.width = 24; // The width of your projectile
            Projectile.height = 24; // The height of your projectile
            Projectile.friendly = true; // Deals damage to enemies
            Projectile.penetrate = -1; // Infinite pierce
            Projectile.DamageType = DamageClass.Melee; // Deals melee damage
            Projectile.usesLocalNPCImmunity = true; // Used for hit cooldown changes in the ai hook
            Projectile.localNPCHitCooldown = 10; // This facilitates custom hit cooldown logic
        }
    }
}
