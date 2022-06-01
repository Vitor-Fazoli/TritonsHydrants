using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace GearonArsenal.Content.Tiles {
    public class AuraliteOre : ModTile {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;

			DustType = DustID.Stone;
			//ItemDrop = ModContent.ItemType<Items.Placeable.ExampleBlock>();

			AddMapEntry(new Color(142, 223, 234));

		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
	}
}