using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TritonsHydrants.Common.Players;

namespace TritonsHydrants.Content.Items.Accessories
{
    /// <summary>
    /// This item is evolutive and have 5 levels, before any tasks to do!
    /// </summary>
    public class TideEmblem : ModItem
    {
        private const int LEVEL_MAX = 5;
        public int level = 0;

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TideEmblemPlayer>().isActive = true;
        }

        public override void LoadData(TagCompound tag)
        {
            level = tag.Get<int>("level");
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("level", level);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }
    }
}