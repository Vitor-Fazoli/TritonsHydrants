using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace VoidArsenal.Content.Projectiles
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
            Projectile.spriteDirection = (int)Projectile.position.AngleTo(Projectile.velocity);
            Projectile.direction = Projectile.spriteDirection;
        }
    }
}
