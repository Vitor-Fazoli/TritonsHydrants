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
            AIType = ProjectileID.SkyFracture;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 3f);
                Dust d = Dust.NewDustPerfect(new Vector2(Projectile.position.X,Projectile.Center.Y), DustID.DemonTorch, speed * 5);
                d.noGravity = true;
            }
        }
    }
}
