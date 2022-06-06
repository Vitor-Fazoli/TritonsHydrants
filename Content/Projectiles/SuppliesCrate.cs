using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Projectiles
{
    public class SuppliesCrate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supplies Crate");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
        }

        public override bool OnPickup(Player player)
        {
            player.statLife += 10;
            Item.active = false;
            return false;
        }
    }
}
