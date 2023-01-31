using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DevilsWarehouse
{
    public static class Helper
    {
        public static double Ticks(double time)
        {
            return time * 60;
        }
        public static float Ticks(float time)
        {
            return time * 60;
        }
        public static int Ticks(int time)
        {
            return time * 60;
        }

        public static Color WaterLocal()
        {
            return Main.waterStyle switch
            {
                2 => new Color(59, 29, 131), //Corruption
                3 => new Color(7, 145, 142), //Jungle
                4 => new Color(171, 11, 209), //Hallow
                5 => new Color(9, 137, 191), //Snow
                6 => new Color(32, 168, 117), //Desert
                7 => new Color(36, 60, 148), //Cavern
                8 => new Color(65, 59, 101), //Cavern 2
                9 => new Color(200, 0, 0), //BloodMoon
                10 => new Color(177, 54, 79), //Crimsom
                12 => new Color(168, 106, 32), //Desert 2 
                _ => new Color(9, 61, 191), // Default
            };
        }

        public static String RemoveBegin(this String str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str.Remove(len);
        }
        public static String RemoveEnd(this String str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str[..^len];
        }
    }
}
