using GearonArsenalMod.Abstract;
using GearonArsenalMod.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace GearonArsenalMod.Content
{
    class BerserkerUI : UIState
    {
        private const float Precent = 0f;
        private UIElement area;
		private UIImage barFrame;
		public override void OnInitialize()
        {
			area = new UIElement();
			area.Left.Set(460, Precent);
			area.Top.Set(25, Precent);
			area.Width.Set(50, Precent);
			area.Height.Set(42, Precent);

			barFrame = new UIImage(ModContent.Request<Texture2D>("GearonArsenalMod/Assets/Textures/GUI/BerserkerUIOne"));
			barFrame.Left.Set(0, Precent);
			barFrame.Top.Set(0, Precent);
			barFrame.Width.Set(50, Precent);
			barFrame.Height.Set(42, Precent);

			area.Append(barFrame);
			Append(area);
		}
		public override void Draw(SpriteBatch spriteBatch)
        {
			if (!(Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword))
                return;

            base.Draw(spriteBatch);
        }
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<GreatswordPlayer>();

			if (!(Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword))
				return;

			if(modPlayer.slayerPower == 0)
            {
				barFrame.SetImage(ModContent.Request<Texture2D>("GearonArsenalMod/Assets/Textures/GUI/BerserkerUIEmpty"));
			}
			else if (modPlayer.slayerPower == 1)
			{
				barFrame.SetImage(ModContent.Request<Texture2D>("GearonArsenalMod/Assets/Textures/GUI/BerserkerUIOne"));
			}
			else if (modPlayer.slayerPower == 2)
			{
				barFrame.SetImage(ModContent.Request<Texture2D>("GearonArsenalMod/Assets/Textures/GUI/BerserkerUITwo"));
			}
			else if (modPlayer.slayerPower == 3)
			{
				barFrame.SetImage(ModContent.Request<Texture2D>("GearonArsenalMod/Assets/Textures/GUI/BerserkerUIThree"));
			}

            if (Main.playerInventory == false)
            {
				area.Left.Set(460, Precent);
				area.Top.Set(25, Precent);
			}
            else
            {
				area.Left.Set(560, Precent);
				area.Top.Set(40, Precent);
			}

			base.Update(gameTime);
		}
	}
}
