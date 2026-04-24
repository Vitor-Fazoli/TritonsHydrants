using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Content.Items.Weapons.Gushers;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Content.Projectiles
{
    public class AquaBurstHeld : ModProjectile
    {
        public override string Texture =>
            "TritonsHydrants/Content/Items/Weapons/Gushers/CopperGusher";

        // ai[0] = charge timer
        // ai[1] = max charge ticks
        // ai[2] = max damage multiplier × 100
        public ref float ChargeTimer => ref Projectile.ai[0];
        public ref float MaxCharge => ref Projectile.ai[1];
        public ref float MaxMultiplierX100 => ref Projectile.ai[2];

        private float ChargeProgress => MathHelper.Clamp(ChargeTimer / MaxCharge, 0f, 1f);
        private bool IsMaxCharge => ChargeTimer >= MaxCharge;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;

            DrawOffsetX = -17;
            DrawOriginOffsetY = 2;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            // Mata se jogador morreu ou trocou de item
            if (player.dead || player.HeldItem.ModItem is not GusherBase)
            {
                Projectile.Kill();
                return;
            }

            // Só roda lógica de carga no dono
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    // Atualiza velocidade (= direção do holdout) para seguir o mouse
                    float holdoutDistance = 10f * Projectile.scale;
                    Vector2 holdoutOffset = holdoutDistance *
                        Vector2.Normalize(Main.MouseWorld - playerCenter);

                    if (holdoutOffset.X != Projectile.velocity.X ||
                        holdoutOffset.Y != Projectile.velocity.Y)
                        Projectile.netUpdate = true;

                    Projectile.velocity = holdoutOffset;

                    // Acumula carga
                    if (ChargeTimer < MaxCharge)
                        ChargeTimer++;

                    SpawnChargeDust();
                }
                else if(ChargeTimer < MaxCharge / 3)
                {
                    Projectile.Kill();
                    return;
                }
                else
                {
                    // Soltou — dispara AquaBurst com dano escalonado
                    float maxMult = MaxMultiplierX100 / 100f;
                    float multiplier = MathHelper.Lerp(1f, maxMult, ChargeProgress);
                    int damage = (int)(Projectile.damage * multiplier);
                    float knockback = Projectile.knockBack * multiplier;

                    Vector2 shootVelocity = Vector2.Normalize(Projectile.velocity) *
                        player.HeldItem.shootSpeed;
                    Vector2 spawnPos = playerCenter +
                        new Vector2(shootVelocity.X * 4, shootVelocity.Y * 4);

                    var source = player.GetSource_ItemUse(player.HeldItem);
                    Projectile.NewProjectile(source, spawnPos, shootVelocity,
                        ModContent.ProjectileType<AquaBurst>(),
                        damage, knockback, Projectile.owner);

                    Projectile.Kill();
                    return;
                }
            }

            // Padrão obrigatório de held projectile (igual ao exemplo)
            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;

            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0f;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

            Projectile.timeLeft = 2;
        }

        private void SpawnChargeDust()
        {
            Vector2 center = Projectile.Center;

            if (IsMaxCharge)
            {
                if (Main.GameUpdateCount % 5 == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 vel = Main.rand.NextVector2CircularEdge(2f, 2f);
                        Dust d = Dust.NewDustDirect(center - new Vector2(8f), 8, 8,
                            DustID.GoldFlame, vel.X, vel.Y, 0, default, 1.4f);
                        d.noGravity = true;
                    }
                }
            }
            else if(ChargeTimer > MaxCharge / 3)
            {
                if (Main.GameUpdateCount % 5 == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 vel = Main.rand.NextVector2CircularEdge(2f, 2f);
                        Dust d = Dust.NewDustDirect(center - new Vector2(8f), 8, 8,
                            DustID.BeachShell, vel.X, vel.Y, 0, default, 1.4f);
                        d.noGravity = true;
                    }
                }
            }
            else
            {
                float chance = MathHelper.Lerp(0.1f, 0.7f, ChargeProgress);
                if (Main.rand.NextFloat() < chance)
                {
                    Vector2 vel = Main.rand.NextVector2CircularEdge(
                        MathHelper.Lerp(0.3f, 1.5f, ChargeProgress),
                        MathHelper.Lerp(0.3f, 1.5f, ChargeProgress));

                    Dust d = Dust.NewDustDirect(center - new Vector2(4f), 8, 8,
                        DustID.Water, vel.X, vel.Y, 100, default,
                        MathHelper.Lerp(0.5f, 1.0f, ChargeProgress));
                    d.noGravity = true;
                }
            }
        }
    }
}