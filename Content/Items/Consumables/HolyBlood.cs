using DevilsWarehouse.Common.Systems.VampireSystem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Consumables
{
    public class HolyBlood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Blood With Mint");

            Tooltip.SetDefault("Now, Without your plague/n" +
                "Remove Vampirism");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.Swiftness, 1);
            Item.value = Item.buyPrice(1, 50);
            Item.rare = ItemRarityID.Red;
        }
        public override void OnConsumeItem(Player player)
        {
            player.GetModPlayer<Vampire>().vampire = false;
        }
    }
}