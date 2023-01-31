using System;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse
{
    public class DevilsWarehouse : Mod
    {
        public static DevilsWarehouse Instance { get; set; }

        public DevilsWarehouse()
        {
            Instance = this;
        }
        public override void Unload()
        {
            if (!Main.dedServ)
            {
                Instance = null;
            }
        }
    }
}