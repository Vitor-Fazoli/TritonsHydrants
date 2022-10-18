using System;

namespace DevilsWarehouse
{
    public static class Readability
    {
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
