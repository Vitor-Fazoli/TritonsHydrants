using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Projectiles
{
    public class AquaBurst : ModProjectile
    {
        private bool HitDirect { get; set; }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.extraUpdates = 75;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 dustVelocity = new Vector2(2, 2).RotatedByRandom(100) * Main.rand.NextFloat(0.1f, 0.8f);

                Dust dust = Dust.NewDustPerfect(Projectile.Center + dustVelocity, Main.rand.NextBool(4) ? 264 : 66, dustVelocity, 0, default, Main.rand.NextFloat(0.9f, 1.2f));
                dust.noGravity = true;
                dust.color = Water.GetWaterColor();
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player owner = Main.player[Projectile.owner];
            owner.velocity += -(Projectile.velocity / 2);
            SoundEngine.PlaySound(SoundID.LiquidsHoneyWater with { Volume = 1.25f, Pitch = 0.6f }, Projectile.Center);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            for (int i = 0; i <= 25; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(4) ? DustID.PortalBoltTrail : DustID.RainbowTorch, (Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f).RotatedByRandom(MathHelper.ToRadians(360f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default,
                    Main.rand.NextFloat(1.2f, 1.6f));

                dust.noGravity = true;
                dust.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }

            for (int k = 0; k < 50; k++)
            {
                Vector2 shootVel = Main.rand.NextVector2Circular(1f, 1f);

                Dust dust2 = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(4) ? DustID.PortalBoltTrail : DustID.RainbowTorch, shootVel);
                dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
                dust2.noGravity = true;
                dust2.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }

            return base.OnTileCollide(oldVelocity);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            for (int i = 0; i <= 8; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(4) ? DustID.PortalBoltTrail : DustID.RainbowTorch, (Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.8f), 0, default,
                    Main.rand.NextFloat(1.2f, 1.6f));
                dust.noGravity = true;
                dust.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }

            for (int k = 0; k < 20; k++)
            {
                Vector2 shootVel = (Projectile.velocity * 20).RotatedByRandom(0.5f) * Main.rand.NextFloat(0.1f, 1.8f);

                Dust dust2 = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(4) ? DustID.PortalBoltTrail : DustID.RainbowTorch, shootVel);
                dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
                dust2.noGravity = true;
                dust2.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(HitDirect);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            HitDirect = reader.ReadBoolean();
        }
    }
}