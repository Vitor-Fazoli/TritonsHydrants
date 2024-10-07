using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TritonsHydrants.Common;

namespace TritonsHydrants.Content.Projectiles
{
    /// <summary>
    /// 
    /// </summary>
    public class IronTridentProj : TridentBase
    {
        protected override float HoldoutRangeMax => 120f;
        public override void SetDefaults()
        {

            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.aiStyle = ProjAIStyleID.Spear;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
            Projectile.alpha = 0;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
    }
}
