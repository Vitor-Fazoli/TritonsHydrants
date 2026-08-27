using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Common.Systems
{
    public abstract class BaseTridentProjectile : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 96f;
        protected virtual float DistanceSpawnProj => 100;
        protected virtual int Proj => ModContent.ProjectileType<AquaticArrow>();
        private bool isHappen = false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * 0.5f;
            float progress;

            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;

                // Spawn aquatic arrow when spear reach your max distance
                if (isHappen is false)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        int proj = Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),          // source correto: "veio deste projétil"
                            Projectile.Center + Projectile.velocity * DistanceSpawnProj,
                            Projectile.velocity * 8f,
                            Proj,
                            Projectile.damage,
                            Projectile.knockBack,
                            Projectile.owner
                        );

                        // Força o servidor a notificar todos os clientes sobre este projétil
                        if (proj >= 0 && proj < Main.maxProjectiles)
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj);
                    }

                    isHappen = true;
                }
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
        }
    }
}