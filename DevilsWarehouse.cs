using DevilsWarehouse.Content.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse
{
    public class DevilsWarehouse : Mod
    {
        public static DevilsWarehouse Instance { get; set; }

        public DevilsWarehouse()
        {
            Instance = this;
        }
        public override void Unload()
        {
            if (!Main.dedServ)
            {
                Instance = null;
            }
        }
    }
    internal class BerserkerSystem : ModSystem
    {

        internal BerserkerUI barActive;

        private UserInterface _barActive;

        public override void Load()
        {

            barActive = new BerserkerUI();
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
                    "YourMod: A Description",
                    delegate {

                        _barActive.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}