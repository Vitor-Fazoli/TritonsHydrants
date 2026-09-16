using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Dusts
{
    public class Notes : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1.5f;
            dust.alpha = 0;
            dust.velocity = new Vector2(0f, -Main.rand.NextFloat(0.6f, 1f));
            // Each note starts at a different point in its sway.
            dust.fadeIn = Main.rand.NextFloat(MathHelper.TwoPi);
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.Cyan * (1f - dust.alpha / 255f);
        }

        public override bool Update(Dust dust)
        {
            dust.fadeIn += 0.08f;
            dust.velocity.X = (float)Math.Cos(dust.fadeIn) * 0.25f;
            dust.position += dust.velocity;
            dust.rotation = (float)Math.Sin(dust.fadeIn) * 0.12f;
            dust.scale -= 0.008f;
            dust.alpha = Math.Min(255, dust.alpha + 3);

            if (dust.alpha >= 255 || dust.scale < 0.1f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}
