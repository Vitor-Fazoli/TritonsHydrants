using Terraria;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class FortressNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fortress Necklace");
            Tooltip.SetDefault("all of your damage now stacks with your defense");
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

        }
    }
}
