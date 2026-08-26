using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Projectiles
{
    public class IronCageP : ModProjectile
    {
        public float ChainLength = 30f;
        private const string ChainTexturePath = "TritonsHydrants/Content/Projectiles/IronChainExtra";
        private const string ChainTextureExtraPath = "TritonsHydrants/Content/Projectiles/IronChain";

        private static Asset<Texture2D> chainTexture;
        private static Asset<Texture2D> chainTextureExtra;

        public override void Load()
        {
            chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);
            chainTextureExtra = ModContent.Request<Texture2D>(ChainTextureExtraPath);
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.5f;
            Projectile.damage = 0;
        }

        public override bool? CanDamage() => false;
        public override void AI()
        {
            Animate();

            DustEffects();

            Projectile.timeLeft = 2;

            if (Main.rand.NextBool(100))
            {
                Vector2 position = new(Projectile.Center.X + Main.rand.NextFloat(-2f, 2f), Projectile.position.Y - 5);
                Vector2 velocity = new(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-2f, 0.5f));

                Projectile.NewProjectile(
                    new EntitySource_TileBreak(2, 2),
                    position,
                    velocity,
                    ModContent.ProjectileType<WaterBubble>(),
                    0,
                    0f,
                    Main.myPlayer
                );
            }

            if (Projectile.ai[1] == 0f && Projectile.ai[2] == 0f)
            {
                Projectile.ai[1] = Projectile.Center.X;
                Projectile.ai[2] = Projectile.Center.Y;
                Projectile.netUpdate = true;
            }

            Vector2 origin = new(Projectile.ai[1], Projectile.ai[2]);

            Projectile.ai[0]++;

            float windEffect = Main.windSpeedCurrent * 1.05f;
            float waveEffect = (float)Math.Sin(Projectile.ai[0] * 0.05f) * 10f;

            float maxChainLength = ChainLength;
            Vector2 targetPosition = origin + new Vector2(waveEffect + windEffect * 30f, -maxChainLength);

            Projectile.Center = Vector2.Lerp(Projectile.Center, targetPosition, 0.05f);

            Vector2 chainVector = Projectile.Center - origin;
            Projectile.rotation = chainVector.ToRotation() + MathHelper.PiOver2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float offsetY = 24f;
            Vector2 origin = new(Projectile.ai[1], Projectile.ai[2]);
            Vector2 vectorFromOrigin = Projectile.Center - origin;
            float remainingLength = vectorFromOrigin.Length() - offsetY;
            Vector2 unitVector = vectorFromOrigin.SafeNormalize(Vector2.Zero);
            float chainRotation = vectorFromOrigin.ToRotation() + MathHelper.PiOver2;

            Vector2 currentDrawPos = origin;
            float segmentLength = 12f;
            int segmentIndex = 0;

            while (remainingLength > 0f)
            {
                Asset<Texture2D> activeTexture = (segmentIndex % 2 == 0) ? chainTexture : chainTextureExtra;
                Color drawColor = Lighting.GetColor((int)(currentDrawPos.X / 16f), (int)(currentDrawPos.Y / 16f));

                Main.spriteBatch.Draw(
                    activeTexture.Value,
                    currentDrawPos - Main.screenPosition,
                    null,
                    drawColor,
                    chainRotation,
                    activeTexture.Size() * 0.5f,
                    1f,
                    SpriteEffects.None,
                    0f
                );

                currentDrawPos += unitVector * segmentLength;
                remainingLength -= segmentLength;
                segmentIndex++;
            }

            Texture2D projectileTexture = TextureAssets.Projectile[Type].Value;
            int frameHeight = projectileTexture.Height / Main.projFrames[Type];
            Rectangle sourceRectangle = new(0, frameHeight * Projectile.frame, projectileTexture.Width, frameHeight);
            Vector2 drawOrigin = sourceRectangle.Size() * 0.5f;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(
                    projectileTexture,
                    drawPos,
                    sourceRectangle,
                    color,
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale - k / (float)Projectile.oldPos.Length / 3,
                    spriteEffects,
                    0f
                );
            }

            return true;
        }

        private void Animate()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Type])
                    Projectile.frame = 0;
            }
        }

        private void DustEffects()
        {
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position / 2, Projectile.width, Projectile.height / 2, DustID.Water, 0f, 0f, 100, default, Projectile.scale);
                dust.noGravity = true;
                dust.velocity *= 0.4f;
            }
        }
    }
}