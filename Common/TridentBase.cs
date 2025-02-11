using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Common
{
    public abstract class TridentBaseProj : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        private Vector2 MousePos = Vector2.Zero;

        /// <summary>
        /// 
        /// </summary>
        protected virtual float HoldoutRangeMin => 24f;

        /// <summary>
        /// 
        /// </summary>
		protected virtual float HoldoutRangeMax => 96f;

        /// <summary>
        /// 
        /// </summary>
        protected virtual float DistanceSpawnProj => 100;

        /// <summary>
        /// 
        /// </summary>
        protected virtual int Proj => ModContent.ProjectileType<AquaticArrow>();

        /// <summary>
        /// this bool makes the projectile happen one time
        /// </summary>
        private bool isHappen = false;

        public override void OnSpawn(IEntitySource source)
        {
            MousePos = Main.MouseWorld;
        }

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
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center + player.Center.DirectionTo(MousePos) * DistanceSpawnProj, Projectile.velocity * 8f, Proj, Projectile.damage,
                        Projectile.knockBack, Projectile.owner);

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
    public abstract class TridentBaseItem : ModItem
    {
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
    }
}
