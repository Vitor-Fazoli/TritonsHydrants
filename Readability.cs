using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.Audio;

namespace DevilsWarehouse
{
    public static class Readability
    {
        public static double ToTicks( double time)
        {
            return time * 60;
        }
        public static float ToTicks(float time)
        {
            return time * 60;
        }
        public static int ToTicks(int time)
        {
            return time * 60;
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
