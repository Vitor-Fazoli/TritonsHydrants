using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using DevilsWarehouse.Content.Buffs;
using Terraria.Graphics.Effects;

namespace DevilsWarehouse.Content.Projectiles
{
    public class RedKing : Warbow
    {
        private int rippleCount = 3;
        private int rippleSize = 5;
        private int rippleSpeed = 15;
        private float distortStrength = 100f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red King");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 96;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 20;
        }
        protected override void Shoot()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.channel && Projectile.timeLeft == 10 && Projectile.ai[0] < 1f)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity * Projectile.ai[0], normalArrow, (int)(Projectile.damage * Projectile.ai[0] / 1.5f), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
            }
            else if (!player.channel && Projectile.timeLeft == 10 && Projectile.ai[0] > 1f)
            {
                player.AddBuff(ModContent.BuffType<RedKingHeart>(), Readability.toTicks(20));
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity * SpecialMultiplier, SpecialArrow, (int)(Projectile.damage * 2), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, Projectile.Center);
            }
        }
    }
}
