using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TritonsHydrants.Common.Systems
{
    public class AtlantisGen : ModSystem
    {
        // Edite esta matriz: 0 = vazio; qualquer outro número = bloco.
        private static readonly int[,] Structure =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int genIndex = tasks.FindIndex(genPass => genPass.Name.Equals("Micro Biomes"));
            if (genIndex >= 0)
                tasks.Insert(genIndex + 1, new PassLegacy("Atlantis Sky Structure", GenerateStructure));
        }

        private static void GenerateStructure(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Erguendo estrutura no céu...";

            int width = Structure.GetLength(1);
            int height = Structure.GetLength(0);
            int originX = Main.maxTilesX / 2 - width / 2;
            int originY = Math.Max(80, (int)Main.worldSurface - 220);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int cell = Structure[row, column];
                    if (cell == 0)
                        continue;

                    int x = originX + column;
                    int y = originY + row;
                    if (!WorldGen.InWorld(x, y, 10))
                        continue;

                    WorldGen.PlaceTile(x, y, TileID.MarbleBlock, forced: true, mute: true);
                    WorldGen.SquareTileFrame(x, y, true);
                }
            }
        }
    }
}