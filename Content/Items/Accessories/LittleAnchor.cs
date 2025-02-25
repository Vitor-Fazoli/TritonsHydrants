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
            Item.width = 10;
            Item.height = 10;

            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 15;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) -= Helper.Percentage(10);
            player.thorns = 0.1f;
        }
    }
}