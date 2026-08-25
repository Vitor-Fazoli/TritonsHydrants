using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TritonsHydrants.Content.Tiles
{
    public class WaterElementalGate : ModTile
    {
        private int projectileInstance;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.DrawYOffset = 4;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 100, 200), CreateMapEntryName());
            DustType = DustID.Water;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = new(i * 16 + 16, j * 16);
                Vector2 velocity = new(0f, -3f);

                projectileInstance = Projectile.NewProjectile(
                    new EntitySource_TileUpdate(i, j),
                    position,
                    velocity,
                    ModContent.ProjectileType<Projectiles.WaterElementalGateProjectile>(),
                    10,
                    0f,
                    Main.myPlayer
                );
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && projectileInstance >= 0 && projectileInstance < Main.maxProjectiles)
            {
                Projectile projectile = Main.projectile[projectileInstance];
                if (projectile.active && projectile.type == ModContent.ProjectileType<Projectiles.WaterElementalGateProjectile>())
                {
                    projectile.Kill();
                }
            }
        }
    }
}