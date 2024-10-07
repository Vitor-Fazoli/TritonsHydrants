using TritonsHydrants.Content.Dusts;
using TritonsHydrants.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Projectiles
{
    public class NeptuneFragment : ModProjectile
    {
        public Vector2 ParentCenter;

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 10;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = true;
            Projectile.netUpdate = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            ParentCenter = Main.projectile[Projectile.owner].Center;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<WaterBubble>(), speed * 5);
                d.noGravity = true;

                Lighting.AddLight(Projectile.position, Water.GetWaterColor().ToVector3());
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Water.GetWaterColor();
            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}
