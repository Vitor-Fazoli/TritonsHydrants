using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TritonsHydrants.Common.Systems;

public class AtlantisGen : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int PilesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Piles"));

        if (PilesIndex != -1)
        {
            tasks.Insert(PilesIndex + 1, new AtlantisGenPass("Example Mod Piles", 100f));
        }
    }
}

public class AtlantisGenPass(string name, float loadWeight) : GenPass(name, loadWeight)
{
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Example Mod Piles";

        int[] tileTypes = [TileID.AncientGoldBrick, TileID.Marble];

        // To not be annoying, we'll only spawn 15 Example Rubble near the spawn point.
        // This example uses the Try Until Success approach: https://github.com/tModLoader/tModLoader/wiki/World-Generation#try-until-success
        for (int k = 0; k < 15; k++)
        {
            bool success = false;
            int attempts = 0;

            while (!success)
            {
                attempts++;
                if (attempts > 1000)
                {
                    break;
                }
                int x = WorldGen.genRand.Next(Main.maxTilesX / 2 - 40, Main.maxTilesX / 2 + 40);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, (int)GenVars.worldSurfaceHigh);
                int tileType = WorldGen.genRand.Next(tileTypes);
                int placeStyle = WorldGen.genRand.Next(6); // Each of these tiles have 6 place styles
                if (Main.tile[x, y].TileType == tileType)
                {
                    continue;
                }

                WorldGen.PlaceTile(x, y, tileType, mute: true, style: placeStyle);
                success = Main.tile[x, y].TileType == tileType;
            }
        }
    }
}