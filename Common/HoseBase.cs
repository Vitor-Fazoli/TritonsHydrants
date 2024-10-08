using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Common
{
    public class HoseBase : ModProjectile
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

        private enum AIState
        {
            WaterCannon,
            WaterLance
        }

        private AIState CurrentAIState
        {
            get => (AIState)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        public ref float StateTimer => ref Projectile.ai[1];
        public ref float CollisionCounter => ref Projectile.localAI[0];
        public ref float SpinningStateTimer => ref Projectile.localAI[1];

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            switch (CurrentAIState)
            {
                case AIState.WaterCannon:

                    break;
                case AIState.WaterLance:

                    break;
            }
        }
    }
}
