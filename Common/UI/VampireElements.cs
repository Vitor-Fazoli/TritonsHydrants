using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    public class VampireSkill : UIElement
    {
        private const float Precent = 0f;
        private readonly string name;
        private readonly UIImage SkillImage;
        private readonly string effect;

        public VampireSkill(string name, UIImage SkillImage, string effect) : base()
        {
            this.name = name;
            this.SkillImage = SkillImage;
            this.effect = effect;
        }

        public override void OnInitialize()
        {
            this.Width.Set(50, Precent);
            this.Height.Set(50, Precent);

            SkillImage.Width.Set(26, Precent);
            SkillImage.Height.Set(26, Precent);
            SkillImage.VAlign = 0.5f;
            SkillImage.HAlign = 0.5f;
            Append(SkillImage);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.instance.MouseText(name + effect);
            }

            base.Update(gameTime);
        }
    }
}
