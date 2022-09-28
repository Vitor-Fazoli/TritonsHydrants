using DevilsWarehouse.Common.Abstract;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.GameContent.Creative;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class Acai : Artifact
    {
        protected override void ModifyTooltipsAgain(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            if (godAscended)
            {
                var line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
                switch (god)
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
        }
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Acai Berry");
            ItemID.Sets.CanGetPrefixes[Item.type] = false;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
            godAscended = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(godAscended)
            {
                switch (god)
                {
                    // Zeus
                    case 0:
                        player.GetDamage(DamageClass.Summon) *= 3f;
                        player.maxMinions = 0;
                        break;
                    // Poseidon
                    case 1:
                        player.GetDamage(DamageClass.Summon) += 0.5f;
                        player.manaRegen = 0;
                        player.manaRegenBonus = 0;
                        player.manaRegenCount = 0;
                        break;
                    // Hades
                    case 2:
                        player.GetDamage(DamageClass.Summon) -= 0.45f;
                        player.maxMinions *= 2;
                        break;
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Grapefruit, 20)
                .AddIngredient(ItemID.Star, 5)
                .AddIngredient(ItemID.CopperBar, 10)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}