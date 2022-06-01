using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using GearonArsenal.Common;

namespace GearonArsenal.Content.Items.Armors
{
    public class WitchMask : ModItem
    {
        public override void SetStaticDefaults()
        {
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
}