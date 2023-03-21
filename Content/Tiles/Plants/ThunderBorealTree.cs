using DevilsWarehouse.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Tiles.Plants
{
    public class ThunderBorealTree : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new() 
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = new int[1] { TileID.IceBlock };
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("DevilsWarehouse/Content/Tiles/Plants/ThunderBorealTree");
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<ThunderBorealSaplingTile>();
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            // This is where fancy code could go, but let's save that for an advanced example
        }

        // Branch Textures
        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("DevilsWarehouse/Content/Tiles/Plants/ThunderBorealTree_Branches");
        }

        // Top Textures
        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("DevilsWarehouse/Content/Tiles/Plants/ThunderBorealTree_Tops");
        }

        public override int DropWood()
        {
            return ModContent.ItemType<AuraliteOre>();
        }

        public override bool Shake(int x, int y, ref bool createLeaves)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ItemID.Elderberry);
            return false;
        }

        public override int TreeLeaf()
        {
            return ModContent.GoreType<ThunderBorealLeaf>();
        }
    }
}
