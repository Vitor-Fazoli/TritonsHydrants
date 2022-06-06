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
            if (player.statLife >= player.statLifeMax2)
            {
                player.statLife += 10;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), CombatText.HealLife, 10);
            }
            


            
            Item.active = false;
            return false;
        }
    }
}
