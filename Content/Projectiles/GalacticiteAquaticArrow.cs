using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Projectiles
{
    public class GalacticiteAquaticArrow : AquaticArrow
    {
        public override void HallowEffect(Projectile projectile)
        {
            Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), projectile.position, projectile.position, 3, projectile.owner, 0, 0);
        }

        public override void JungleEffect(Projectile projectile)
        {
            base.JungleEffect(projectile);
        }

        public override void DesertEffect(Projectile projectile)
        {
            base.DesertEffect(projectile);
        }

        public override void CavernEffect(Projectile projectile)
        {
            base.CavernEffect(projectile);
        }

        public override void CrimsomEffect(Projectile projectile)
        {
            base.CrimsomEffect(projectile);
        }

        public override void CorruptionEffect(Projectile projectile)
        {
            base.CorruptionEffect(projectile);
        }

        public override void SnowEffect(Projectile projectile)
        {
            base.SnowEffect(projectile);
        }

        public override void ForestEffect(Projectile projectile)
        {
            base.ForestEffect(projectile);
        }
    }
}