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
            player.GetModPlayer<TideNecklacePlayer>().tideNecklace = true;
            player.breathMax = 400;
            player.accFlipper = true;
            player.ignoreWater = true;
            player.waterWalk = true;


            if (player.wet)
            {
                player.breath = player.breathMax;
                player.moveSpeed *= 1.20f;
                player.breathCD--;
            }
            else
            {
                player.moveSpeed *= 0.70f;
            }
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
        public override void OnEnterWorld(Player player)
        {
            if (tideNecklace)
            {
                player.breath = player.breathMax;
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (tideNecklace)
            {
                if(Player.wet == false)
                {
                    if (Player.lifeRegen > 0)
                        Player.lifeRegen = 0;

                    Player.lifeRegenTime = 0;

                    Player.lifeRegen -= 1;
                }
            }
        }
        public override void ResetEffects()
        {
            tideNecklace = false;
        }
    }
}
