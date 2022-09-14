using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;

namespace VoidArsenal.Content.Projectiles
{
    public class RedKing : Warbow
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red King");
            Main.projFrames[Projectile.type] = 3;
        }
    }
}
