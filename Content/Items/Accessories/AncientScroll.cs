using DevilsWarehouse.Common.Systems.JutsuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Back)]
    public class AncientScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("With great scrolls comes great responsibility");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = player.GetModPlayer<JutsuPlayer>();

            player.moveSpeed += 0.15f;
            p.chakraMax2 += 120;
        }
    }
}
