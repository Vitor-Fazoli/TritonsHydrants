using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Items.Accessories
{
    public class LittleAnchor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 19;

            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) -= Helper.Percentage(10);
            player.thorns = 0.1f;
        }
    }
}