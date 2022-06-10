using GearonArsenal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Projectiles.Minions
{
    public class WanderingAuralite : HoverShooter
    {
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 3;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.damage = 30;
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
			shoot = ProjectileID.WoodenArrowFriendly;
			shootSpeed = 12;
		}

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			Summoner modPlayer = player.GetModPlayer<Summoner>();
			if (player.dead)
			{
				modPlayer.WanderingAuraliteMinion = false;
			}
			if (modPlayer.WanderingAuraliteMinion)
			{ 
				Projectile.timeLeft = 2;
			}
		}

		public override void CreateDust()
		{
			if (Projectile.ai[0] == 0f)
			{
				if (Main.rand.NextBool(5))
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.AmberBolt);
					Main.dust[dust].velocity.Y -= 1.2f;
				}
			}
			else
			{
				if (Main.rand.NextBool(3))
				{
					Vector2 dustVel = Projectile.velocity;
					if (dustVel != Vector2.Zero)
					{
						dustVel.Normalize();
					}
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.AmberBolt);
					Main.dust[dust].velocity -= 1.2f * dustVel;
				}
			}
			Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
		}

		public override void SelectFrame()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 3;
			}
		}
	}
}
