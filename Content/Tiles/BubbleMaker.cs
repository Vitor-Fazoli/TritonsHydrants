using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

//TODO: Create an Entity with a water creature which spawns bubbles when it moves, and make it so that the player can interact with it to get a buff or something. The bubbles should be projectiles that float up and pop after a while, creating a visual effect.
namespace TritonsHydrants.Content.Tiles
{
    public class BubbleMaker : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            // Configuração 3x2
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 100, 200), CreateMapEntryName());
            DustType = DustID.Water;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Main.tile[i, j];

            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Water, 0f, -2f);
                Main.dust[dust].noGravity = true;
            }

            if (tile.TileFrameX == 18 && tile.TileFrameY == 0)
            {
                if (Main.rand.NextBool(15))
                {
                    Vector2 position = new(i * 16 + 8, j * 16);
                    Vector2 velocity = new(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-3f, -1f));

                    Projectile.NewProjectile(
                        new EntitySource_TileUpdate(i, j),
                        position,
                        velocity,
                        ModContent.ProjectileType<Projectiles.WaterBubble>(),
                        0,
                        0f,
                        Main.myPlayer
                    );
                }
            }
        }
    }
}