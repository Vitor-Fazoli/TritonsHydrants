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
    public class VampireButtom : UIState
    {
        private const float Precent = 0f;
        private UIImage frame;
        public override void OnInitialize()
        {
            frame = new UIImage(ModContent.Request<Texture2D>("DevilsWarehouse/Assets/Textures/VampireButtom"));
            frame.Left.Set(385, Precent);
            frame.Top.Set(255, Precent);
            frame.Width.Set(100, Precent);
            frame.Height.Set(50, Precent);

            Append(frame);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<Vampire>();
            if (!player.vampire && !Main.playerInventory)
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
            if (!player.vampire && !Main.playerInventory)
                return;

            if (frame.IsMouseHovering)
            {
                frame.SetImage(ModContent.Request<Texture2D>("DevilsWarehouse/Assets/Textures/VampireButtomHover"));
            }
            else
            {
                frame.SetImage(ModContent.Request<Texture2D>("DevilsWarehouse/Assets/Textures/VampireButtom"));
            }

            base.Update(gameTime);
        }
        public override void Click(UIMouseEvent evt)
        {
            
        }
    }
    class VampireButtomUISystem : ModSystem
    {
        private UserInterface VampireButtomInterface;

        internal VampireButtom VampireButtom;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                VampireButtom = new();
                VampireButtomInterface = new();
                VampireButtomInterface.SetState(VampireButtom);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            VampireButtomInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate
                    {
                        if (Main.playerInventory)
                        {
                            VampireButtomInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}


