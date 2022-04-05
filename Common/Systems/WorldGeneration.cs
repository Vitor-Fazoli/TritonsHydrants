using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;
using GearonArsenal.Content.Tiles;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.IO;
using System;

namespace GearonArsenal.Common.Systems {
    internal class WorldGeneration : ModSystem {
        private void GenerationFungusBiome() {
            int x = WorldGen.genRand.Next(0, Main.maxTilesX);
            int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(100, 300), WorldGen.genRand.Next(90, 100), ModContent.TileType<FungalDirt>());
            }
        }
        private void GenerationOre(GenerationProgress progress, GameConfiguration configuration) {

            progress.Message = "ores 1";

            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {

                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 8), WorldGen.genRand.Next(1, 6), TileID.Titanium);
            }
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {

            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1) {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("World Gen Tutorial Ores", GenerationOre));
            }
        }
        public override void PostWorldGen() {
            GenerationFungusBiome();
        }
    }
}
