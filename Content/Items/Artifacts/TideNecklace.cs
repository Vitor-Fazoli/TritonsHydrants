using DevilsWarehouse.Common.Systems;
using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class TideNecklace : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PaladinsShield)
                .AddIngredient(ItemID.IceMachine)
                .AddIngredient(ItemID.IronskinPotion, 10)
                .AddIngredient(ItemID.CopperBar, 40)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
    internal class TideNecklacePlayer : ModPlayer
    {
        public bool tideNecklace = false;
        public override void PreUpdate()
        {
            if (tideNecklace)
            {
                if (Player.wet)
                {
                    
                }
            }
        }
        public override void ResetEffects()
        {
            tideNecklace = false;
        }
    }
}
