using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TritonsHydrants.Content.Items.Materials;

namespace TritonsHydrants.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class RoyalHelmetOfTheDeep : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.GoldChainmail && legs.type == ItemID.GoldGreaves;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CrownFragment>(3)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}