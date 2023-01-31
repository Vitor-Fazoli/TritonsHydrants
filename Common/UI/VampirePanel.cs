using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Humanizer.In;

namespace DevilsWarehouse.Common.UI
{
    public class VampirePanelState : UIState
    {
        public override void OnInitialize()
        {
            UIPanel panel = new();
            panel.Width.Set(300, 0);
            panel.Height.Set(600, 0);
            panel.HAlign = panel.VAlign = 0.5f;
            panel.BackgroundColor = new(23,23,23, 100);
            Append(panel);

            UIText header = new("Vampirism");
            header.Top.Set(30, 0);
            header.HAlign = 0.5f;
            panel.Append(header);

            UIImage title = new UIImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireTitle"));
            title.Width.Set(88, 0);
            title.Height.Set(12, 0);
            title.Top.Set(0, 0);
            title.HAlign = 0.5f;
            title.ImageScale = 2f;
            panel.Append(title);


            UIImage one = new UIImage(ModContent.Request<Texture2D>(Helper.GUIPath + "skill"));
            one.Width.Set(88, 0);
            one.Height.Set(12, 0);
            one.Top.Set(0, 0);
            one.HAlign = 0.5f;
            one.ImageScale = 2f;
            panel.Append(one);

        } 
    }
    public class VampirePanelSystem : ModSystem
    {
        internal VampirePanelState vampirePanel;
        internal UserInterface _vampirePanel;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _vampirePanel = new UserInterface();

                vampirePanel = new VampirePanelState();
                vampirePanel.Activate();
            }
        }
        public override void Unload()
        {
            vampirePanel = null;
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (_vampirePanel?.CurrentState != null)
            {
                vampirePanel.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    Mod.DisplayName + "Vampire Interface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _vampirePanel?.CurrentState != null)
                        {
                            _vampirePanel.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
        internal void ShowHideVampirePanel()
        {
            if (_vampirePanel?.CurrentState == null)
            {
                _vampirePanel?.SetState(vampirePanel);
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
            else
            {
                _vampirePanel?.SetState(null);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }

        }
    }

}
