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
    public class WaterElementalGateProjectile : ModProjectile
    {
        private const string ChainTexturePath = "TritonsHydrants/Content/Projectiles/ExampleAdvancedFlailProjectileChain";
        private const string ChainTextureExtraPath = "TritonsHydrants/Content/Projectiles/ExampleAdvancedFlailProjectileChainExtra";

        private static Asset<Texture2D> chainTexture;
        private static Asset<Texture2D> chainTextureExtra;

        public override void Load()
        {
            chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);
            chainTextureExtra = ModContent.Request<Texture2D>(ChainTextureExtraPath);
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;

            if (Main.rand.NextBool(30))
            {
                Vector2 position = new(Projectile.position.X, Projectile.position.Y - 5);
                Vector2 velocity = new(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-3f, -1f));

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

            // Define a posição de origem (base do tile) no primeiro tick
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

            // Posição alvo mantida acima da base presa pela corrente
            float maxChainLength = 50f;
            Vector2 targetPosition = origin + new Vector2(waveEffect + windEffect * 30f, -maxChainLength);

            // Suaviza o movimento em direção ao alvo preso à corrente
            Projectile.Center = Vector2.Lerp(Projectile.Center, targetPosition, 0.05f);

            // Rotação acompanhando a inclinação da corrente
            Vector2 chainVector = Projectile.Center - origin;
            Projectile.rotation = chainVector.ToRotation() + MathHelper.PiOver2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin = new(Projectile.ai[1], Projectile.ai[2]);
            Vector2 vectorFromOrigin = Projectile.Center - origin;
            float remainingLength = vectorFromOrigin.Length() - 12f; // Ajuste para o comprimento do projétil
            Vector2 unitVector = vectorFromOrigin.SafeNormalize(Vector2.Zero);
            float chainRotation = vectorFromOrigin.ToRotation() + MathHelper.PiOver2;

            Vector2 currentDrawPos = origin;
            float segmentLength = 12f;
            int segmentIndex = 0;

            // Desenha as correntes alternando entre os dois sprites a cada segmento
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

            // Desenha o projétil principal com rastro
            Texture2D projectileTexture = TextureAssets.Projectile[Type].Value;
            Vector2 drawOrigin = new(projectileTexture.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length / 3, spriteEffects, 0f);
            }

            return true;
        }
    }
}