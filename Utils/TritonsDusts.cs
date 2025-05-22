using Microsoft.Xna.Framework;
using Terraria;

namespace TritonsHydrants.Utils;

public class TritonsDusts
{
    public static void DustWater(Vector2 position, int type, float scale = 1f)
    {
        Dust dust = Dust.NewDustPerfect(position, type, null, 0, default, scale);
        dust.noGravity = true;
        dust.color = Water.GetWaterColor();
    }

    public static void DustWater(Vector2 position, int type, Vector2 velocity, float scale = 1f)
    {
        Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, default, scale);
        dust.noGravity = true;
        dust.color = Water.GetWaterColor();
    }
}