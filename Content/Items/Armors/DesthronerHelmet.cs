using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class DesthronerHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            // Dust.NewDust(player.Center + new Vector2(0, player.height / 2), 5, 5, ModContent.DustType<ArcanePowder>(), -player.velocity.X / 4, 0);
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "reduces mana cost by 10%";
            player.manaCost -= 0.1f;
        }
    }
}