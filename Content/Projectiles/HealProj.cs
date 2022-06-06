using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Projectiles
{
    internal class HealProj : ModProjectile
    {
        public override bool PreAI()
        {
			if (Projectile.aiStyle == 52)
			{
				Player player = Main.player[(int)Projectile.ai[0]];
				Vector2 center = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
				float offsetX = player.Center.X - center.X;
				float offsetY = player.Center.Y - center.Y;
				float distance = (float)Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
				if (distance < 50f && Projectile.position.X < player.position.X + player.width && Projectile.position.X + Projectile.width > player.position.X && Projectile.position.Y < player.position.Y + player.height && Projectile.position.Y + Projectile.height > player.position.Y)
				{
					if (Projectile.owner == Main.myPlayer && !Main.LocalPlayer.moonLeech)
					{
						int heal = (int)Projectile.ai[1];
						int damage = player.statLifeMax2 - player.statLife;
						if (heal > damage)
						{
							heal = damage;
						}
						if (heal > 0)
						{
							player.AddBuff(BuffID.Regeneration, 2 * heal, false);
						}
					}
				}
			}
			return base.PreAI();
		}
    }
}
