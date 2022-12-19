using DevilsWarehouse.Common.Systems.VampireSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    class VampireUI : UIState
    {
        private const float Precent = 0f;
        private UIImage point;
        private UIImage frame;
        public override void OnInitialize()
        {
            point = new UIImage(ModContent.Request<Texture2D>("DevilsWarehouse/Assets/Textures/VampireBlood"));
            point.Left.Set(0, Precent);
            point.Top.Set(0, Precent);
            point.Width.Set(50, Precent);
            point.Height.Set(42, Precent);

            frame = new UIImage(ModContent.Request<Texture2D>("DevilsWarehouse/Assets/Textures/VampireBloodFrame"));
            frame.Left.Set(460, Precent);
            frame.Top.Set(20, Precent);
            frame.Width.Set(50, Precent);
            frame.Height.Set(42, Precent);

            frame.Append(point);
            Append(frame);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Vampire>();
            if (!player.vampire)
                return;

            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Vampire>();

            float quotient = (float)player.blood / player.bloodMax;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            Rectangle hitbox = frame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 30;
            hitbox.Height -= 40;


        }
        public override void Update(GameTime gameTime)
        {
            var player = Main.LocalPlayer.GetModPlayer<Vampire>();

            float quotient = (float)player.blood / player.bloodMax;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            if (!player.vampire)
                return;


            if (Main.playerInventory == false)
            {
                frame.Left.Set(460, Precent);
                frame.Top.Set(20, Precent);
                point.ImageScale = quotient;
            }
            else
            {
                frame.Left.Set(560, Precent);
                frame.Top.Set(35, Precent);
                point.ImageScale = quotient;
            }

            if (player.blood < player.bloodMax)
            {
                if (frame.IsMouseHovering)
                    Main.instance.MouseText(player.blood + " %", 0, 0);
            }
            else
            {
                if (frame.IsMouseHovering)
                    Main.instance.MouseText("Max");
            }

            base.Update(gameTime);
        }
    }
}
