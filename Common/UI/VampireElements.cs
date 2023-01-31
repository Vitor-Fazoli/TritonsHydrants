using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    public class VampireSkill : UIElement
    {
        private string name;
        private UIImage SkillImage;
        private string effect;

        public VampireSkill(string name, UIImage SkillImage, string effect) : base()
        {
            this.name = name;
            this.SkillImage = SkillImage;
            this.effect = effect;
        }

        public override void OnInitialize()
        {
            Width.Set(50, 0);
            Height.Set(50, 0);

            SkillImage.Width = Width;
            SkillImage.Height = Height;
            SkillImage.VAlign = 0.5f;
            SkillImage.HAlign = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.instance.MouseText(name + "\n" + effect);
            }
        }
    }

    public class VampirePassive : UIElement
    {
    
    }
}
