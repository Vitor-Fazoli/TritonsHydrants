using DevilsWarehouse.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

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