using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Dusts
{
    public class ArcanePowder : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
            dust.scale -= 0.015f;
            dust.alpha = 50;

            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            else
            {
                float strength = dust.scale / 2f;
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.color.R / 255f * 0.5f * strength, dust.color.G / 255f * 0.5f * strength, dust.color.B / 255f * 0.5f * strength);
            }
            return false;
        }
    }
}
