//using GearonArsenal.Common;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Content.Items.Consumables
//{
//    internal class SacrificialStone : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Sacrificial Stone");
//            Tooltip.SetDefault("Uma poderosa pedra para desbloquear os poderes de artefato");
//        }
//        //public override void AddRecipes()
//        //{
//        //    CreateRecipe()
//        //        .AddIngredient(ItemID.EbonstoneBlock, 50)
//        //        .AddIngredient();
//        //}
//        public override bool ConsumeItem(Player player)
//        {
//            CommonPlayer cp = new CommonPlayer();

//            if (cp.artifactSlotEnable == false)
//            {
//                cp.artifactSlotEnable = true;
//                return true;
//            }

//            return false;
//        }
//    }
//}