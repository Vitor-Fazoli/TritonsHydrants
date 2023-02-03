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
            int offset = 60;

            List<UIPanel> panels = new();

            UIPanel panel = new();
            panel.Width.Set(300, 0);
            panel.Height.Set(500, 0);
            panel.HAlign = panel.VAlign = 0.5f;
            panel.BackgroundColor = new(23, 23, 23, 100);
            Append(panel);

            UIText header = new("Vampirism");
            header.Top.Set(30, 0);
            header.HAlign = 0.5f;
            panel.Append(header);

            UIImage title = new (ModContent.Request<Texture2D>(Helper.GUIPath + "VampireTitle"));
            title.Width.Set(88, 0);
            title.Height.Set(22, 0);
            title.Top.Set(0, 0);
            title.HAlign = 0.5f;
            title.ImageScale = 2f;
            panel.Append(title);

            for (int i = 0; i <= 5; i++)
            {
                UIPanel row = new();
                row.Width.Set(panel.Width.Pixels, 0);
                row.Height.Set(offset, 0);
                row.SetPadding(0);
                row.Top.Set(100 + (offset * i), 0);
                row.HAlign = 0.5f;
                row.BackgroundColor = new(0, 0, 0, 0);
                row.BorderColor = new(0, 0, 0, 255);
                panel.Append(row);
                panels.Add(row);
            }
            for (int i = 0; i <= 5; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    //UIImage vs = new(ModContent.Request<Texture2D>(Helper.GUIPath + "skill"));
                    //vs.Width.Set(26, 0);
                    //vs.Height.Set(26, 0);
                    //vs.Top.Set(0, 0);
                    //vs.Left.Set((vs.Width.Pixels + 50) * j, 0);
                    //vs.HAlign = 0.5f;
                    //vs.VAlign = 0.5f;
                    //panels[i].Append(vs);
                   
                    VampireSkill vs = new("Power guido", new(ModContent.Request<Texture2D>(Helper.GUIPath + "skill")),"faz o pau maior");
                    vs.Width.Set(26, 0);
                    vs.Height.Set(26, 0);
                    vs.Top.Set(0, 0);
                    vs.Left.Set((vs.Width.Pixels + 50) * j, 0);
                    vs.HAlign = 0.5f;
                    vs.VAlign = 0.5f;
                    panels[i].Append(vs);
                }
            }
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
