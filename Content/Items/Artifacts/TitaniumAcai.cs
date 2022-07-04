using GearonArsenal.Common.Abstract;
using GearonArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class TitaniumAcai : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Acai Berry");
            Tooltip.SetDefault("Increase your defense,\n" +
                "Double your summon damage but you can't have minions");
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactR>();
            Item.defense = 10;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 1f;
            player.maxMinions = 0;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Acai>(1)
                .AddIngredient(ItemID.TitaniumBar, 15)
                .AddIngredient(ItemID.HallowedBar, 3)
                .AddIngredient(ItemID.SoulofMight,1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}