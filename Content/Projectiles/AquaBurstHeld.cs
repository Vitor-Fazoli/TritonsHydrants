using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;

namespace TritonsHydrants.Content.Projectiles
{
    public class AquaBurstHeld : ModProjectile
    {
        public override string Texture =>
            "TritonsHydrants/Content/Items/Weapons/Gushers/CopperGusher";

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
            Projectile.width = 58;
            Projectile.height = 22;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || player.HeldItem.ModItem is not GusherBase)
            {
                Projectile.Kill();
                return;
            }

            player.heldProj = Projectile.whoAmI;

            // Velocity sempre normalizada — representa só direção, nunca magnitude
            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            if (Main.myPlayer == Projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.MountedCenter);
                    if (direction != Projectile.velocity)
                    {
                        Projectile.velocity = direction;
                        Projectile.netUpdate = true;
                    }

                    if (ChargeTimer < MaxCharge)
                        ChargeTimer++;

                    SpawnChargeDust(player);
                }
                else
                {
                    float maxMult = MaxMultiplierX100 / 100f;
                    float multiplier = MathHelper.Lerp(1f, maxMult, ChargeProgress);
                    int damage = (int)(Projectile.damage * multiplier);
                    float knockback = Projectile.knockBack * multiplier;

                    Vector2 shootVelocity = Projectile.velocity * player.HeldItem.shootSpeed;
                    Vector2 spawnPos = player.MountedCenter + Projectile.velocity * 50f;

                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(player.HeldItem),
                        spawnPos, shootVelocity,
                        ModContent.ProjectileType<AquaBurst>(),
                        damage, knockback, Projectile.owner);

                    Projectile.Kill();
                    return;
                }
            }

            Projectile.Center = player.MountedCenter + Projectile.velocity * 8f;

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.SetDummyItemTime(2);

            Projectile.rotation = Projectile.velocity.ToRotation();

            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

            // Braço — compensa a direção do jogador no ângulo
            float armRotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRotation);

            Projectile.timeLeft = 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            // origin = (0, height/2) significa que o pivot de rotação
            // é a borda esquerda da sprite, centralizada verticalmente
            // = onde a mão do jogador segura a mangueira
            Vector2 origin = new(30f, texture.Height / 2f);

            SpriteEffects flip = player.direction == -1
                ? SpriteEffects.FlipVertically
                : SpriteEffects.None;

            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                flip,
                0);

            return false;
        }

        private void SpawnChargeDust(Player player)
        {
            // Dust na ponta da sprite (base + comprimento total = 14 + 58 = ~72px)
            Vector2 center = player.MountedCenter + Projectile.velocity * 60f;

            if (IsMaxCharge)
            {
                if (Main.GameUpdateCount % 5 == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 vel = Main.rand.NextVector2CircularEdge(2f, 2f);
                        Dust d = Dust.NewDustDirect(center - new Vector2(4f), 8, 8,
                            DustID.GoldFlame, vel.X, vel.Y, 0, default, 1.4f);
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