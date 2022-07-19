using VoidArsenal.Common.Abstract;
using VoidArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class Acai : Artifact
    {
        protected override void ModifyCreation(List<TooltipLine> tooltips)
        {
            var line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
            switch (Gem)
            {
                case 0:
                    line.Text = "Triple your summon damage but you dont have minion";
                    break;
                case 1:
                    line.Text = "Increase your summon damage but you dont have mana regen";
                    break;
                case 2:
                    line.Text = "Deacreased your summon damage but you have double of minions";
                    break;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acai Berry");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            switch (Gem)
            {
                // Ruby
                case 0:
                    player.GetDamage(DamageClass.Summon) *= 3f;
                    player.maxMinions = 0;
                    break;
                // Sapphire
                case 1:
                    player.GetDamage(DamageClass.Summon) += 0.5f;
                    player.manaRegen = 0;
                    player.manaRegenBonus = 0;
                    player.manaRegenCount = 0;
                    break;
                // Emerald
                case 2:
                    player.GetDamage(DamageClass.Summon) -= 0.45f;
                    player.maxMinions *= 2;
                    break;
            }
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