using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using DevilsWarehouse.Content.Dusts;

namespace DevilsWarehouse.Content.Projectiles
{
    public abstract class Warbow : ModProjectile
    {
        // Specific Attributes
        protected float channelTime = 0.005f;
        protected int damage;
        protected int normalArrow = ProjectileID.WoodenArrowFriendly;
        protected int SpecialArrow = ProjectileID.JestersArrow;
        protected int SpecialMultiplier = 3;

        private int rippleCount = 3;
        private int rippleSize = 5;
        private int rippleSpeed = 15;
        private float distortStrength = 100f;

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
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

            player.ConsumeItem(ItemID.WoodenArrow);
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[1] = 1;

            HoldingWeapon();
            Channeling();
            Shoot();

            Vector2 dir = Projectile.velocity;
            dir.Normalize();
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2 + dir * -30;
            Projectile.rotation = dir.ToRotation() + (Projectile.direction < 0 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }



        public override void AI()
        {

        }
        protected virtual void HoldingWeapon()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 rotationPoint = player.RotatedRelativePoint(player.MountedCenter, true);

            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale * (player.archery ? 1.2f : 1);
                        Projectile.ai[1] = 40f / player.inventory[player.selectedItem].useTime;
                    }
                    Vector2 toDestination = Main.MouseWorld - rotationPoint;
                    toDestination.Normalize();

                    if (toDestination.HasNaNs())
                    {
                        toDestination = Vector2.UnitX * player.direction;
                    }
                    toDestination *= scaleFactor;
                    if (toDestination.X != Projectile.velocity.X || toDestination.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = toDestination;
                }
            }
            else
            {
                Projectile.Kill();
            }
        }
        protected virtual void Channeling()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 rotationPoint = player.RotatedRelativePoint(player.MountedCenter, true);
            if (player.channel && !player.noItems && !player.CCed)
            {
                if (Projectile.ai[0] < 1)
                {
                    Projectile.ai[0] += Projectile.ai[1] * channelTime;
                }
                else if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                }
                Projectile.timeLeft = 11;
            }

            if (Projectile.ai[0] < 0.5f || Projectile.timeLeft < 10)
            {
                var dust = Dust.NewDustPerfect(rotationPoint, 6, Projectile.velocity / 2, newColor: Color.Yellow, Scale: 1f);
                dust.noGravity = true;
                dust.fadeIn = 0;
            }
            else if (Projectile.ai[0] < 1f)
            {
                var dust = Dust.NewDustPerfect(rotationPoint, 6, Projectile.velocity, newColor: Color.Orange, Scale: 1.5f);
                dust.noGravity = true;
                dust.fadeIn = 0;
            }
            else
            {
                var dust = Dust.NewDustPerfect(rotationPoint, 6, Projectile.velocity * 2, newColor: Color.Red, Scale: 1.5f);
                dust.noGravity = true;
                dust.fadeIn = 0;
            }
        }
        protected virtual void Shoot()
        {
            Projectile.ai[0] = 1; // Set state to exploded
            Projectile.alpha = 255; // Make the Projectile invisible.
            Projectile.friendly = false; // Stop the bomb from hurting enemies.

            if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
            }


            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
                float progress = (180f - Projectile.timeLeft) / 60f;
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
            }

            Player player = Main.player[Projectile.owner];

            if (!player.channel && Projectile.timeLeft == 10 && Projectile.ai[0] < 1f)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity * Projectile.ai[0], normalArrow, (int)(Projectile.damage * Projectile.ai[0] / 1.5f), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
            }
            else if (!player.channel && Projectile.timeLeft == 10 && Projectile.ai[0] > 1f)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Projectile.Center, Projectile.velocity * SpecialMultiplier, SpecialArrow, (int)(Projectile.damage * 2), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, Projectile.Center);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
    }
}
