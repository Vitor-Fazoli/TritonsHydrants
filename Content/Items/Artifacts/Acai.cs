using VoidArsenal.Common.Abstract;
using VoidArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class Acai : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acai Berry");
            Tooltip.SetDefault("Double your summon damage but you can't have minions");
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
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
                .AddIngredient(ItemID.DirtBlock, 20)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}