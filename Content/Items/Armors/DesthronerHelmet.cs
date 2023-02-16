using DevilsWarehouse.Content.Items.Materials;
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
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Increase throwing critical chance");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Throwing) += 0.05f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DesthronerBreastplate>() && legs.type == ModContent.ItemType<DesthronerLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases dealt damage by 10%";
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DesthronerScale>(3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}