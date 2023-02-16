using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Items.Weapons.Summon.Staffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles.Minions
{
    public class LivingCore : ModProjectile
    {
        public int projIndex;
        public int quantity;
        public int quantityMax;
        private int distance;
        public override void Load()
        {
            projIndex = Projectile.type;
            distance = 60;
            quantity = 0;
            quantityMax = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(FullName));

            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            // These below are needed for a minion
            // Denotes that this Projectile is a pet or minion
            Main.projPet[Projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 17;
            Projectile.height = 17;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;

            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.friendly = true;
            // Only determines the damage type
            Projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.minionSlots = 1f;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.penetrate = -1;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return false;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];

            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (p.dead || !p.active)
            {
                p.ClearBuff(ModContent.BuffType<WanderingAuraliteBuff>());
            }
            if (p.HasBuff(ModContent.BuffType<WanderingAuraliteBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            #endregion

            #region General behavior

            if (distance <= 0 && quantity <= quantityMax)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), this.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LivingShard>(), 5, 20, p.whoAmI);
                //player.AddBuff(ModContent.BuffType<IceOrbBuff>(), 2);
                distance = 360 / quantityMax;
                quantity++;
            }

            distance--;

            Vector2 idlePosition = p.Center;
            idlePosition.Y -= 60f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is Projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -p.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == p.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the Projectile,
                // and then set netUpdate to true
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];
                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X) Projectile.velocity.X -= overlapVelocity;
                    else Projectile.velocity.X += overlapVelocity;

                    if (Projectile.position.Y < other.position.Y) Projectile.velocity.Y -= overlapVelocity;
                    else Projectile.velocity.Y += overlapVelocity;
                }
            }
            #endregion

            #region Movement

            float speed = 16f;
            float inertia = 20f;


            if (distanceToIdlePosition > 600f)
            {
                // Speed up the minion if it's away from the player
                speed = 20f;
                inertia = 60f;
            }
            else
            {
                // Slow down the minion if closer to the player
                speed = 8f;
                inertia = 80f;
            }
            if (distanceToIdlePosition > 20f)
            {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                // If there is a case where it's not moving at all, give it a little "poke"
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }

            #endregion

            #region Animation and visuals
            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.08f;

            // Some visuals here
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
            #endregion
        }
    }
}
