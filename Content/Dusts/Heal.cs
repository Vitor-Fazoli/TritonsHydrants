using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Dusts
{
    public class Heal : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1.5f;
        }

        public override bool Update(Dust dust)
        {
            dust.scale -= 0.015f;
            dust.alpha = 50;

            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}
