using VoidArsenal.Common.Abstract;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class WitchHeart : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Witch Mask");
            Tooltip.SetDefault("This mask have a curse, if are using this your life its your mana\n" +
                "increased life max in 20 \n" +
                "your life regen is increased");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.defense = 3;
            Item.accessory = true;
            Item.scale = 0.7f;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 20;
            player.lifeRegen += 10;

            player.GetModPlayer<WitchHeartPlayer>().witchHeart = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BattlePotion, 10)
                .AddIngredient(ItemID.WaterCandle, 2)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.WizardHat)
                .AddTile(TileID.LivingLoom)
                .Register();
        }
    }
    /// <summary>
    /// This class below make possible a new form to play with a magic, you turn a blood mage and use your vitality
    /// </summary>
    internal class WitchHeartPlayer : ModPlayer
    {
        public bool witchHeart = false;
        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (witchHeart)
            {
                reduce -= 0.5f;
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (witchHeart)
                Player.statLife -= manaConsumed;

            Player.statMana = Player.statLifeMax2;
        }
        public override void ResetEffects()
        {
            witchHeart = false;
        }
    }
    /// <summary>
    /// Part of a big change, your items change when you are wearing Witch Mask
    /// </summary>
    public class WitchHeartSystem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            //making a player dont kill yourself with a witch mask
            if (item.DamageType == DamageClass.Magic && player.GetModPlayer<WitchHeartPlayer>().witchHeart == true)
            {
                if (player.statLife <= item.mana)
                {
                    return false;
                }
            }
            return base.CanUseItem(item, player);
        }
    }
}