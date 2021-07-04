using Terraria.ModLoader;
using GreatswordsMod.Attack;
using GreatswordsMod.Abstract;


namespace GreatswordsMod.Weapon
{
    public class SilverGreatswordP : Greatsword
    {
        public override void SetDefaults()
        {
            //properties - Default
            projectile.width = 0;
            projectile.height = 0;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 124;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates = 1;

            //properties - Greatsword
            dmg = 3;
            cooldown = 60;
            proj = ModContent.ProjectileType<SilverSlash>();
        } 
    }
}
