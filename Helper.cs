using Microsoft.Xna.Framework;
using System;
using System.Text;
using Terraria;

namespace DevilsWarehouse
{
    public static class Helper
    {
        public const string GUIPath = "DevilsWarehouse/Assets/GUI/";

        public static NPC FindClosestNPC(float maxDetectDistance, Projectile proj)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, proj.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public static string ToDisplay(string str)
        {
            StringBuilder sb = new();

            for(int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]) && i != 0)
                {
                    sb.Append(' ');
                    sb.Append(str[i]);
                }
                else
                {
                    sb.Append(str[i]);
                }
            }
            return sb.ToString();
        }
        public static int PercentOf(float quantity, int max)
        {
            return (int)(max * quantity);
        }
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


        public static string RemoveBegin(this string str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str.Remove(len);
        }
        public static string RemoveEnd(this string str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str[..^len];
        }
    }
    //<summary> This is used for make easy and visible the process with properties of water in terraria</summary>
    public static class Water
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

        public static Color WaterColor()
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
    }
}
