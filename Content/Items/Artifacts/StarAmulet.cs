using DevilsWarehouse.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class StarAmulet : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Your mana is your shield");
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
            player.GetModPlayer<StarAmuletPlayer>().starAmulet = true;
        }
        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //        .AddIngredient(ItemID.PaladinsShield)
        //        .AddIngredient(ItemID.IceMachine)
        //        .AddIngredient(ItemID.IronskinPotion, 10)
        //        .AddIngredient(ItemID.CopperBar, 40)
        //        .AddTile(TileID.Hellforge)
        //        .Register();
        //}
    }
    internal class StarAmuletPlayer : ModPlayer
    {
        public bool starAmulet = false;
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (starAmulet)
            {
                if (damage >= Player.statMana)
                {
                    Player.statMana -= (int)damage;
                    CombatText.NewText(Player.getRect(), Color.Purple, (int)damage);
                }
                else if (damage <= Player.statMana)
                {
                    int leftover = (int)damage - Player.statMana;
                    Player.statMana = 0;
                    Player.statLife -= leftover;
                }

                if (Player.statMana <= 0)
                {
                    Player.statMana = 0;
                }
            }


        }
        public override void ResetEffects()
        {
            starAmulet = false;
        }
    }
}