using DevilsWarehouse.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Body)]
    public class DesthronerBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Increase throwing attack speed");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Throwing) += 0.05f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DesthronerScale>(5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
