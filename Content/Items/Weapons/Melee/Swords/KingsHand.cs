using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Weapons.Melee.Swords
{
    public class KingsHand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King's Hand");
        }
        public override void SetDefaults()
        {
            Item.width = 78;
            Item.height = 76;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 10;
            Item.damage = 10;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }
        public override void HoldItem(Player player)
        {

        }
    }
}
