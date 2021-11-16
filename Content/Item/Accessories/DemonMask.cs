//using GearonArsenalMod.Common.Players;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace GearonArsenalMod.Content.Item.Accessories
//{
//    public class DemonMask : ModItem
//    {
//        private bool equip = false;
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Demon Mask");
//            Tooltip.SetDefault("Decreased Fury Counter but Increased buff time\n[only for greatswords]");
//        }
//        public override void SetDefaults()
//        {
//            Item.accessory = true;
//            Item.rare = ItemRarityID.LightRed;
//        }
//        public override void UpdateAccessory(Player player, bool hideVisual)
//        {
//            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();
//            modPlayer.slayerMax -= 1;
//        }
//    }
//}