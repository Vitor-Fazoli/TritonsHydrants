using Terraria;
using Microsoft.Xna.Framework;
namespace MagicTridents.Utils
{
    public class Water
    {
        public const int Corruption = 2;
        public const int Jungle = 3;
        public const int Hallow = 4;
        public const int Snow = 5;
        public const int Desert = 6;
        public const int Cavern = 7;
        public const int Cavern2 = 8;
        public const int BloodMoon = 9;
        public const int Crimsom = 10;
        public const int Desert2 = 12;

        public static Color GetWaterColor()
        {
            return Main.waterStyle switch
            {
                Corruption => new Color(59, 29, 131),
                Jungle => new Color(7, 145, 142),
                Hallow => new Color(171, 11, 209),
                Snow => new Color(9, 137, 191),
                Desert => new Color(32, 168, 117),
                Cavern => new Color(36, 60, 148),
                Cavern2 => new Color(65, 59, 101),
                BloodMoon => new Color(200, 0, 0),
                Crimsom => new Color(177, 54, 79),
                Desert2 => new Color(168, 106, 32),
                _ => new Color(9, 61, 191), // Default
            };
        }

        public static int getRandomWater()
        {
            return Main.rand.Next(2, 13);
        }
    }
}