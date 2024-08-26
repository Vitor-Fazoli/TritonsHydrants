using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NeptunesTreasure.Common;

namespace NeptunesTreasure.Content.Projectiles
{
    /// <summary>
    /// 
    /// </summary>
    public class UnholyTridentProj : MagicTridentProj
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
