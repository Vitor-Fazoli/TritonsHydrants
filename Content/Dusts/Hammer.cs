using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Dusts
{
    public class Hammer : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 14, 14);
			dust.scale = 2f;
			dust.alpha = 100;
		}

		public override bool Update(Dust dust)
		{
			dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
			dust.scale -= 0.05f;

			if (dust.customData != null && dust.customData is Player player)
			{
				dust.position = player.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
			}

			if (dust.scale < 0.25f)
				dust.active = false;

			return false;
		}
	}
}
