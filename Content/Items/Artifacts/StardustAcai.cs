using GearonArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class StardustAcai : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Acai Berry");
            Tooltip.SetDefault("Increase your defense,\n" +
                "Increase your summon damage too much, you can have 1 minions");
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<Common.Abstract.ArtifactRarity>();
            Item.defense = 10;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 1.50f;
            player.maxMinions = 1;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<TitaniumAcai>(1)
                .AddIngredient(ItemID.FragmentStardust, 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient<AdamantiteAcai>(1)
                .AddIngredient(ItemID.FragmentStardust, 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}