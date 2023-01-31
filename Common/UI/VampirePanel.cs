using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    public class VampirePanelState : UIState
    {
        public override void OnInitialize()
        {
            UIPanel panel = new();
            panel.Width.Set(300, 0);
            panel.Height.Set(600, 0);
            panel.Left.Set(700, 0);
            panel.Top.Set(200, 0);
            panel.BackgroundColor = new(53, 18, 1);
            panel.BorderColor = new(205,133,26);
            Append(panel);

            UIText header = new("Vampirism");
            header.Top.Set(30, 0);
            header.HAlign = 0.5f;
            panel.Append(header);

            UIPanel button = new();
            button.Width.Set(100, 0);
            button.Height.Set(50, 0);
            button.HAlign = 0.5f;
            button.Top.Set(90, 0);
            panel.Append(button);

            UIText text = new("Click me!");
            text.HAlign = text.VAlign = 0.5f;
            button.Append(text);

            UIImage title = new UIImage(ModContent.Request<Texture2D>(Helper.GUIPath + "VampireTitle"));
            title.Width.Set(88, 0);
            title.Height.Set(12, 0);
            title.Top.Set(0, 0);
            title.HAlign = 0.5f;
            title.ImageScale = 2f;
            panel.Append(title);

            VampireSkill one = new("SkillFour");
            one.Top.Set(100, 0);
            one.HAlign = 0.5f;
            panel.Append(one);

            VampireSkill two = new("SkillSix");
            two.Top.Set(160, 0);
            two.HAlign = 0.5f;
            panel.Append(two);

            VampireSkill three = new("SkillThree");
            three.Top.Set(210, 0);
            three.HAlign = 0.5f;
            panel.Append(three);

            VampireSkill four = new("SkillTwo");
            four.Top.Set(260, 0);
            four.HAlign = 0.5f;
            panel.Append(four);

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
