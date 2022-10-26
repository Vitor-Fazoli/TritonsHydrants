using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DevilsWarehouse.Content.Items.Materials
{
    public class Silencer : ModItem
    {
        public virtual string FrameTexture => "DevilsWarehouse/Assets/Default.png";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
        public override bool? CanConsumeBait(Player player)
        {
            return true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color ItemColor, Vector2 origin, float scale)
        {
            Draw(spriteBatch, position + Request<Texture2D>(Texture).Value.Size() / 2 * scale, 1, true);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float opacity, bool glow)
        {
            Texture2D outlineTex = Request<Texture2D>(Texture).Value;
            spriteBatch.Draw(outlineTex, position, null, Color.White * opacity, 0, outlineTex.Size() / 2, 1, 0, 0);
            Texture2D mainTex = Request<Texture2D>(Texture).Value;
            spriteBatch.Draw(mainTex, position, null, Color.White * opacity, 0, mainTex.Size() / 2, 1, 0, 0);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Color color = Main.DiscoColor;

            float rot = Main.rand.NextFloat((float)Math.PI * 2);
            Dust d = Dust.NewDustPerfect(Item.Center + Vector2.One.RotatedBy(rot) * 16, 264, Vector2.One.RotatedBy(rot) * -1.25f, 0, color, 0.8f);
            d.noGravity = true;
            d.noLight = true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Draw(spriteBatch, Item.Center - Main.screenPosition, 1, true);
            return false;
        }
    }
}
