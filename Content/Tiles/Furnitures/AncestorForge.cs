using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DevilsWarehouse.Content.Tiles.Furnitures
{
	public class AncestorForge : ModTile
	{
		public override void SetStaticDefaults()
		{
            // Properties
            Main.tileTable[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;

            DustType = DustID.Sandstorm;
			AdjTiles = new int[] { TileID.Anvils, TileID.Furnaces };

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            // Etc
            ModTranslation name = CreateMapEntryName();
			name.SetDefault("Ancestor Forge");
			AddMapEntry(new Color(200, 200, 200), name);
		}
        public override void NumDust(int x, int y, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 16, ModContent.ItemType<Items.Placeable.AncestorForge>());
        }
    }
}
