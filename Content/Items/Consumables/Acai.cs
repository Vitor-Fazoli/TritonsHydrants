using GearonArsenal.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Consumables
{
    public class Acai : ModItem
    {
        private bool isEnable = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acai Berry");
            Tooltip.SetDefault("Uma poderosa fruta para desbloquear os poderes de artefato");
        }
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTime = 25;
            Item.useAnimation = 25;
        }
        public override bool ConsumeItem(Player player)
        {
            if (!isEnable)
            {
                isEnable = true;
                return true;
            }
            else
            {
                return false;
            }


        }
        public override bool CanUseItem(Player player)
        {
            if (!isEnable)
            {
                ArtifactSlot slot = new();
                slot.enable = true;
                isEnable = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}