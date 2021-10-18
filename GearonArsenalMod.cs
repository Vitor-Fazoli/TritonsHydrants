using GearonArsenalMod.Interface;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace GearonArsenalMod
{
    internal class GearonArsenalMod : Mod
    {


        public GearonArsenalMod()
        {
        }

        public override void Load()
        {

        }

        public override void Unload()
        {
            
        }
    }
    internal class BerserkerSystem : ModSystem{

        internal BerserkerUI barActive;

        private UserInterface _barActive;

        public override void Load(){

            barActive = new BerserkerUI();
            barActive.Activate();
            _barActive = new UserInterface();
            _barActive.SetState(barActive);
        }
        public override void UpdateUI(GameTime gameTime){

            _barActive?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers){

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1){

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate{

                        _barActive.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}