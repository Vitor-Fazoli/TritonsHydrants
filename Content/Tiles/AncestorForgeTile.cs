using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GearonArsenalMod.Content.Tiles
{
	public class AncestorForgeTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileTable[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			DustType = DustID.Sandstorm;
			AdjTiles = new int[] { TileID.Furnaces };

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);

			// Etc
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Ancestor Forge");
			AddMapEntry(new Color(200, 200, 200), name);
		}
        public override void NumDust(int x, int y, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
