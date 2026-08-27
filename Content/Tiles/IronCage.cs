using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace TritonsHydrants.Content.Tiles
{
    public class IronCageEntity : ModTileEntity
    {
        public int ChainLevel = 1;
        public int ProjectileInstance = -1;

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(ChainLevel)] = ChainLevel;
        }

        public override void LoadData(TagCompound tag)
        {
            ChainLevel = tag.GetInt(nameof(ChainLevel));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(ChainLevel);
        }

        public override void NetReceive(BinaryReader reader)
        {
            ChainLevel = reader.ReadInt32();
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<IronCage>();
        }

        public void SpawnOrUpdateProjectile()
        {
            if (ProjectileInstance >= 0 && ProjectileInstance < Main.maxProjectiles)
            {
                Projectile p = Main.projectile[ProjectileInstance];
                if (p.active && p.type == ModContent.ProjectileType<Projectiles.IronCageP>())
                    return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient) return;

            Vector2 position = new(Position.X * 16 + 16, Position.Y * 16);
            Vector2 velocity = new(0f, -3f);

            ProjectileInstance = Projectile.NewProjectile(
                new EntitySource_TileUpdate(Position.X, Position.Y),
                position,
                velocity,
                ModContent.ProjectileType<Projectiles.IronCageP>(),
                10,
                0f,
                Main.myPlayer
            );

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.TileEntitySharing, number: ID);
        }
    }

    public class IronCage : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.DrawYOffset = 4;
            TileObjectData.newTile.HookPostPlaceMyPlayer = ModContent.GetInstance<IronCageEntity>().Generic_HookPostPlaceMyPlayer;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 100, 200), CreateMapEntryName());
            DustType = DustID.Iron;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (TileEntity.TryGet(i, j, out IronCageEntity entity))
            {
                entity.SpawnOrUpdateProjectile();
                Mod.Logger.Debug($"IronCageEntity at ({i}, {j}) has ChainLevel: {entity.ChainLevel} and ProjectileInstance: {entity.ProjectileInstance}");
            }
        }

        public override bool RightClick(int i, int j)
        {
            if (!TileEntity.TryGet(i, j, out IronCageEntity entity)) return true;

            if (entity.ProjectileInstance >= 0 && entity.ProjectileInstance < Main.maxProjectiles)
            {
                Projectile p = Main.projectile[entity.ProjectileInstance];
                if (p.active && p.type == ModContent.ProjectileType<Projectiles.IronCageP>())
                {
                    entity.ChainLevel = entity.ChainLevel < 5 ? entity.ChainLevel + 2 : 1;
                    (p.ModProjectile as Projectiles.IronCageP).ChainLength = 30f * entity.ChainLevel;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.TileEntitySharing, number: entity.ID);
                }
            }

            return true;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (TileEntity.TryGet(i, j, out IronCageEntity entity))
            {
                Projectile p = Main.projectile[entity.ProjectileInstance];
                if (p.active && p.type == ModContent.ProjectileType<Projectiles.IronCageP>())
                    p.Kill();

                entity.Kill(i, j);
            }
        }
    }
}