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
}
