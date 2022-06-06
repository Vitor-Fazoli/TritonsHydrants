using Terraria;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class WitchMask : ModItem
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
            Item.rare = ModContent.RarityType<Artifact>();
            Item.defense = 3;
            Item.accessory = true;
            Item.scale = 0.7f;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 20;
            player.lifeRegen += 10;

            player.GetModPlayer<Witch>().witchMask = true;
        }
    }
    /// <summary>
    /// This class below make possible a new form to play with a magic, you turn a blood mage and use your vitality
    /// </summary>
    public class Witch : ModPlayer
    {
        public bool witchMask = false;
        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (witchMask)
            {
                reduce -= 0.5f;
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (witchMask)
                Player.statLife -= manaConsumed;

            Player.statMana = Player.statLifeMax2;
        }
    }
    /// <summary>
    /// Part of a big change, your items change when you are wearing Witch Mask
    /// </summary>
    public class WitchSystem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            //making a player dont kill yourself with a witch mask
            if (item.DamageType == DamageClass.Magic && player.GetModPlayer<Witch>().witchMask == true)
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