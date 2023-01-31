using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace DevilsWarehouse.Content.Projectiles
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

            Projectile.CloneDefaults(ProjectileID.Starfury);
            AIType = ProjectileID.Starfury;
        }
        public override bool PreAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            return true;
        }

        public override void Kill(int timeLeft)
        {
            const int MAX_DUST = 15;

            for (int i = 0; i < MAX_DUST; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1.5f, 1.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, speed * 5);
                d.noGravity = true;
                d.scale = 2f;
                d.color = Color.MediumPurple;
            }
            for (int i = 0; i < MAX_DUST; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f,1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DemonTorch, speed * 5);
                d.noGravity = true;
                d.scale = 2f;
                d.color = Color.DarkMagenta;
            }

            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
