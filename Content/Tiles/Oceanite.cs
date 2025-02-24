using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TritonsHydrants.Content.Tiles
{
    public class Oceanite : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 410;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0, 255, 255), name);

            DustType = 84;
            HitSound = SoundID.Tink;
            MineResist = 3f;

            RegisterItemDrop(ModContent.ItemType<Items.Placeables.OceaniteOre>());
        }


        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.Blue;
            return true;
        }
    }

    public class OceaniteOreSystem : ModSystem
    {
        public static LocalizedText OceaniteOrePassMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            OceaniteOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(OceaniteOrePassMessage)}");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new OceaniteOrePass("Oceanite Ores", 237.4298f));
            }
        }
    }

    public class OceaniteOrePass(string name, float loadWeight) : GenPass(name, loadWeight)
    {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = OceaniteOreSystem.OceaniteOrePassMessage.Value;

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.beachSandJungleExtraWidth, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Oceanite>());
            }
        }
    }
}