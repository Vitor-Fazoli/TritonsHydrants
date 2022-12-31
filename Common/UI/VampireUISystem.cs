using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    internal class VampireUISystem : ModSystem
    {

        internal VampireUI barActive;

        private UserInterface _barActive;

        public override void Load()
        {

            barActive = new VampireUI();
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
                    "Devil's Warehouse: making a interface for vampire",
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
