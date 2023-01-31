using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    public class VampireSkill : UIElement
    {
        private string image;
        private object _text;
        private UIElement.MouseEvent _clickAction;
        private UIImage _uiImage;

        public VampireSkill(string text) : base()
        {
            image = text;
        }

        public override void OnInitialize()
        {
            _uiImage = new(ModContent.Request<Texture2D>(Helper.GUIPath + image));              
            _uiImage.Width = StyleDimension.Fill;   
            _uiImage.Height = StyleDimension.Fill;
            _uiImage.ImageScale = 0.5f;
            Append(_uiImage);

            _uiImage.OnClick += _clickAction;       
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_text != null)
            {

            }
        }
    }

    public class VampirePassive : UIElement
    {
    
    }
}
