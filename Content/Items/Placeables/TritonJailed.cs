// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using TritonsHydrants.Content.Tiles;

// namespace TritonsHydrants.Content.Items.Placeables
// {
//     public class TritonJailed : ModItem
//     {
//         public override void SetStaticDefaults()
//         {
//             Item.ResearchUnlockCount = 100;
//             ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
//             ItemID.Sets.OreDropsFromSlime[Type] = (3, 13);
//         }

//         public override void SetDefaults()
//         {
//             Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TritonJailed>());
//             Item.width = 12;
//             Item.height = 12;
//             Item.value = 3000;
//         }
//     }
// }