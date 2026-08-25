using Microsoft.Xna.Framework;
using Terraria;

namespace TritonsHydrants.Utils;

public static class TritonsHelper
{
    public static int Ticks(int seconds) => seconds * 60;
    public static float Percentage(float value) => value / 100;
    
    public static Vector2 Parable(Vector2 origin, Vector2 target, float tempoDeVoo) {
        var vx = (target.X - origin.X) / tempoDeVoo;
        var vy = (float)((target.Y - origin.Y) / tempoDeVoo - (0.5 * -9.8f * tempoDeVoo));
        return new Vector2(vx, vy);
    }
}