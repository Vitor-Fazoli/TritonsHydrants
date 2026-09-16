using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TritonsHydrants.Utils;

public class TritonsDusts
{
    public static void DustWater(Vector2 position, int type, float scale = 1f)
    {
        Dust dust = Dust.NewDustPerfect(position, type, null, 0, default, scale);
        dust.noGravity = true;
        dust.color = Water.GetColor();
    }

    public static void DustWater(Vector2 position, int type, Vector2 velocity, float scale = 1f)
    {
        Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, default, scale);
        dust.noGravity = true;
        dust.color = Water.GetColor();
    }

    public static int GetWaterDust()
    {
        return Main.rand.NextBool(4) ? DustID.PortalBoltTrail : DustID.RainbowTorch;
    }
}