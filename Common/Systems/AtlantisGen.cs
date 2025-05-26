using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

public class AtlantisGen : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
        if (genIndex != -1)
        {
            tasks.Insert(genIndex + 1, new PassLegacy("Atlantis Biome", AtlantisPass));
        }
    }

    private void AtlantisPass(GenerationProgress progress, GameConfiguration configuration)
    {

        progress.Message = "Erguendo Atlântida...";

        int W = Main.maxTilesX;
        bool dungeonLeft = Main.dungeonX < W / 2;

        int startX = dungeonLeft ? W - 450 : 300;
        int endX = dungeonLeft ? W - 300 : 450;
        int centerX = (startX + endX) / 2;

        // Altura aproximada do mar (use worldSurface como proxy)
        int waterY = (int)Main.worldSurface + Main.rand.Next(400, 500);

        // Raio da cúpula
        int radius = (endX - startX) / 2;
        int centerY = waterY + radius / 2;

        // 1) Desenha a casca da cúpula (mármore + wall)
        for (int x = -radius; x <= radius; x++)
        {
            int absX = centerX + x;
            if (!WorldGen.InWorld(absX, centerY)) continue;

            // altura máxima pra este x (metade de círculo)
            int h = (int)Math.Sqrt(radius * radius - x * x);

            // para dar espessura, desenhamos dois círculos concêntricos
            for (int dy = -h; dy <= -h + 5; dy++)
            {
                int y = centerY + dy;
                if (!WorldGen.InWorld(absX, y)) continue;

                // Bloco de frente
                WorldGen.PlaceTile(absX, y, TileID.MarbleBlock, forced: true, mute: true);
                // Parede de fundo
                WorldGen.PlaceWall(absX, y, WallID.Marble, true);
            }
        }

        WorldGen.SquareTileFrame(centerX - radius + 4, centerY - radius + 4, true);

        // 3) Arco de entrada e piso de mármore
        int archWidth = radius / 3;
        int archHeight = radius / 2;
        int archX0 = centerX - archWidth / 2;
        int archY0 = centerY + (int)(radius * 0.3f);

        // Piso
        for (int x = archX0 - 2; x <= archX0 + archWidth + 2; x++)
            WorldGen.PlaceTile(x, archY0 + 1, TileID.Dirt, forced: true);

        // Lados do arco
        for (int y = archY0 - archHeight; y <= archY0; y++)
        {
            WorldGen.PlaceTile(archX0, y, TileID.MarbleBlock, forced: true);
            WorldGen.PlaceTile(archX0 + archWidth, y, TileID.MarbleBlock, forced: true);
        }
        // Parte superior do arco (sem interior)
        for (int x = archX0; x <= archX0 + archWidth; x++)
            WorldGen.PlaceTile(x, archY0 - archHeight, TileID.MarbleBlock, forced: true);

        // 4) Colunas internas (duas laterais)
        int colOffset = archWidth / 3;
        for (int side = -1; side <= 1; side += 2)
        {
            int cx = centerX + side * colOffset;
            for (int y = archY0 - archHeight + 2; y <= archY0; y += 2)
                WorldGen.PlaceTile(cx, y, TileID.MarbleColumn, forced: true);
        }

        // 5) Videiras e lanternas
        for (int i = 0; i < 20; i++)
        {
            // Swinging vines
            int vx = Main.rand.Next(centerX - radius + 10, centerX + radius - 10);
            int vy = Main.rand.Next(centerY - radius + 5, centerY - 5);
            WorldGen.PlaceTile(vx, vy, TileID.JungleVines, forced: true);

            // Lanternas subaquáticas (sea lantern)
            if (Main.rand.NextBool(3))
            {
                int lx = Main.rand.Next(centerX - radius + 5, centerX + radius - 5);
                int ly = Main.rand.Next(centerY - radius / 2, centerY + radius / 4);
                WorldGen.PlaceTile(lx, ly, TileID.ChineseLanterns, forced: true);
            }
        }

        // 6) Pequena árvore acima da cúpula
        int treeX = centerX;
        int treeY = centerY - radius - 4;
        WorldGen.TileRunner(treeX, treeY, strength: 6, steps: 12, TileID.JungleGrass, addTile: true);
        WorldGen.GrowTree(treeX, treeY);

        // 7) Ajuste final: alisa bordas externas com TileRunner de mármore
        WorldGen.TileRunner(centerX, centerY, strength: radius / 2, steps: radius, TileID.MarbleBlock, addTile: true);
    }
}