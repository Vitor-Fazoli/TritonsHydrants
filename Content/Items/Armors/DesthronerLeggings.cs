using DevilsWarehouse.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Armors
{
    [AutoloadEquip(EquipType.Legs)]
    public class DesthronerLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Increase throwing critical chance");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green; 
            Item.defense = 4;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Throwing) += 0.1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DesthronerScale>(4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
