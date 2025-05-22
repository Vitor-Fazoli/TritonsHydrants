namespace TritonsHydrants.Utils;

public static class TritonsHelper
{
    public static int Ticks(int seconds) => seconds * 60;
    public static float Percentage(float value) => value / 100;
}