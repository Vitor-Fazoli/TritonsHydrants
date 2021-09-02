using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Dusts
{
    public class CursedSkull : ModDust
    {
        public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 30, 30);
			dust.alpha = 100;
			dust.scale = 0.3f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.scale -= 0.01f;

			if (dust.scale < 0.1f)
				dust.active = false;

			return false;
		}
	}
}
