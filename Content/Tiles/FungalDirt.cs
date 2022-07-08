using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Tiles {
    public class FungalDirt : ModTile {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;

			//DustType = ModContent.DustType<Sparkle>();
			//ItemDrop = ModContent.ItemType<Items.Placeable.ExampleBlock>();

			AddMapEntry(new Color(175, 10, 15));

		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
	}
}