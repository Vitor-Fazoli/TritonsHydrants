using GearonArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class AdamantiteAcai : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Acai Berry");
            Tooltip.SetDefault("Increase your defense,\n" +
                "Double your summon damage but you can't have minions");
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<Artifact>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 1f;
            player.maxMinions = 0;
            player.statDefense += 10;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FruitJuice, 20)
                .AddIngredient(ItemID.Wood, 100)
                .AddIngredient(ItemID.BeeWax, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}