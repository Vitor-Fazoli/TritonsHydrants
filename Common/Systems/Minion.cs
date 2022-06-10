using Terraria.ModLoader;

namespace GearonArsenal.Common.Systems
{
    public abstract class Minion  : ModProjectile
    {
        public override void AI()
        {
            CheckActive();
            Behavior();
        }

        public abstract void CheckActive();

        public abstract void Behavior();
    }
}
