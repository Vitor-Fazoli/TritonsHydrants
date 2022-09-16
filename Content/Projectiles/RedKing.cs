using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using VoidArsenal.Content.Buffs;

namespace VoidArsenal.Content.Projectiles
{
    public class RedKing : Warbow
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red King");
            Main.projFrames[Projectile.type] = 3;
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
                player.AddBuff(ModContent.BuffType<KingsBlessing>(), 180);
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity * SpecialMultiplier, SpecialArrow, (int)(Projectile.damage * 2), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, Projectile.Center);
            }
        }
    }
}
