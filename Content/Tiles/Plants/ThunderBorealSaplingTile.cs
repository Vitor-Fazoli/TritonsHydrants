using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.WorldBuilding;

namespace DevilsWarehouse.Content.Tiles.Plants
{
    internal class ThunderBorealSaplingTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.IceBlock};
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.StyleMultiplier = 3;

            TileObjectData.addTile(Type);
            
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Thunder Boreal Sapling");
            AddMapEntry(new Color(200, 200, 200), name);

            TileID.Sets.TreeSapling[Type] = true;
            TileID.Sets.CommonSapling[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;

            DustType = DustID.BorealWood;

            AdjTiles = new int[] { ModContent.TileType<ThunderBorealSaplingTile>() };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void RandomUpdate(int i, int j)
        {

            bool growSucess;

            growSucess = WorldGen.GrowTree(i, j);


            bool isPlayerNear = WorldGen.PlayerLOS(i, j);

            if (growSucess && isPlayerNear)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (i % 2 == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
        }


    }
    public class ExampleOreSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int PlantingTrees = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));

            if (PlantingTrees != -1)
            {
                tasks.Insert(PlantingTrees + 1, new ThunderBorealPass("Thunder Boreal Trees", 237.4298f));
            }
        }
    }

    public class ThunderBorealPass : GenPass
    {
        public ThunderBorealPass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Thunder Boreal Trees";


            // Criando a malha de tiles
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];

                    if (tile.HasTile && tile.TileType == TileID.IceBlock && !tile.BottomSlope && !tile.TopSlope && !tile.IsHalfBlock && Main.rand.NextBool(10))
                    {
                        // Colocar uma árvore aleatória neste local
                        WorldGen.PlaceTile(x, y - 1, ModContent.TileType<ThunderBorealSaplingTile>());
                    }
                }
            }
        }
    }
}
