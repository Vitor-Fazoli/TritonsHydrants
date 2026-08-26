using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TritonsHydrants.Content.Tiles
{
    public class IronCage : ModTile
    {
        private int projectileInstance = -1;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.DrawYOffset = 4;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 100, 200), CreateMapEntryName());
            DustType = DustID.Iron;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (projectileInstance >= 0 && projectileInstance < Main.maxProjectiles) return;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = new(i * 16 + 16, j * 16);
                Vector2 velocity = new(0f, -3f);

                projectileInstance = Projectile.NewProjectile(
                    new EntitySource_TileUpdate(i, j),
                    position,
                    velocity,
                    ModContent.ProjectileType<Projectiles.IronCageP>(),
                    10,
                    0f,
                    Main.myPlayer
                );
            }
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (projectileInstance >= 0 && projectileInstance < Main.maxProjectiles)
                {
                    Projectile projectile = Main.projectile[projectileInstance];
                    if (projectile.active && projectile.type == ModContent.ProjectileType<Projectiles.IronCageP>())
                    {
                        projectile.Kill();
                    }
                }

                Vector2 position = new(i * 16 + 16, j * 16);
                Vector2 velocity = new(0f, -3f);

                projectileInstance = Projectile.NewProjectile(
                    new EntitySource_TileUpdate(i, j),
                    position,
                    velocity,
                    ModContent.ProjectileType<Projectiles.IronCageP>(),
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
                if (projectile.active && projectile.type == ModContent.ProjectileType<Projectiles.IronCageP>())
                {
                    projectile.Kill();
                }
            }
        }
    }
}