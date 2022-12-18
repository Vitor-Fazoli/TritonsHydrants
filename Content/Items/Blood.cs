using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Items.Consumables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items
{
    public class Blood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supply Crate");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
        }

        public override bool OnPickup(Player player)
        {
            player.GetModPlayer<Vampire>().blood++;

            Item.active = false;
            return false;
        }
    }
}
