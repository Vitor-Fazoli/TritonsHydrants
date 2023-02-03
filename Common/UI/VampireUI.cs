using DevilsWarehouse.Common.Systems.VampireSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    class VampireUIState : UIState
    {
        private const float Precent = 0f;
        private UIImage point;
        private UIImage frame;
        private UIText text;

        public override void OnInitialize()
        {
            point = new UIImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireBlood"));
            point.Left.Set(0, Precent);
            point.Top.Set(0, Precent);
            point.Width.Set(50, Precent);
            point.Height.Set(42, Precent);

            frame = new UIImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireBloodFrame"));
            frame.Left.Set(460, Precent);
            frame.Top.Set(20, Precent);
            frame.Width.Set(50, Precent);
            frame.Height.Set(42, Precent);

            text = new UIText("");
            text.Width.Set(50, Precent);
            text.Height.Set(42, Precent);
            text.Left.Set(0, Precent);
            text.Top.Set(55, Precent);

            frame.Append(point);
            frame.Append(text);
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

            if (player.blood <= player.bloodMax)
            {
                text.SetText(player.blood + "%");
            }
            else
            {
                text.SetText("Max");
            }


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


            if (frame.IsMouseHovering)
            {
                Main.instance.MouseText("Click to open skill tree", 0, 0);
                frame.SetImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireBloodFrameHover"));
            }
            else
            {
                frame.SetImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireBloodFrame"));
            }

            base.Update(gameTime);
        }
        public override void Click(UIMouseEvent evt)
        {
            if (frame.IsMouseHovering)
            {
                ModContent.GetInstance<VampirePanelSystem>().ShowHideVampirePanel();
            }
        }
    }
    internal class VampireUISystem : ModSystem
    {

        internal VampireUIState barActive;

        private UserInterface _barActive;

        public override void Load()
        {

            barActive = new VampireUIState();
            barActive.Activate();
            _barActive = new UserInterface();
            _barActive.SetState(barActive);
        }
        public override void UpdateUI(GameTime gameTime)
        {

            _barActive?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    Mod.DisplayName + ": making a interface for vampire",
                    delegate
                    {

                        _barActive.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
