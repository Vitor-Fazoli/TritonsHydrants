using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Materials
{
    public class AdamantiteFragment : ModItem
    {
        public float durability;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Fragment");
            Tooltip.SetDefault("a long story...");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.material = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 20);
            Item.stack = 99;
        }
        public override bool OnPickup(Player player)
        {
            player.AddBuff(BuffID.Lucky, Readability.toSeconds(5));
            return true;
        }
        public override bool? CanBurnInLava() => false;
    }
}
