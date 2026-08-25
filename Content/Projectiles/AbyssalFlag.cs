using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Projectiles
{
	public class AbyssalFlag : ModProjectile
	{
		public const float Gravity = 0.50f;

		public bool IsStickingToTarget
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int TargetWhoAmI
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public int GravityDelayTimer
		{
			get => (int)Projectile.ai[2];
			set => Projectile.ai[2] = value;
		}

		public float StickTimer
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			UpdateAlpha();
			if (IsStickingToTarget)
			{
				StickyAI();
			}
			else
			{
				NormalAI();
			}
			UpdateDrawLayer();
		}

		private void NormalAI()
		{
			// Gravidade constante da parábola (mesmo valor usado em Item.Shoot para calcular a trajetória)
			Projectile.velocity.Y += Gravity;

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			if (Main.rand.NextBool(3))
			{
				var dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Adamantite, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}

			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Adamantite, 0, 0, 254, Scale: 0.3f);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}
		}

		private const int StickTime = 60 * 15;
		private void StickyAI()
		{
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			StickTimer += 1f;

			bool hitEffect = StickTimer % 30f == 0f;
			int npcTarget = TargetWhoAmI;
			if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
			{
				Projectile.Kill();
			}
			else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
			{
				Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
				Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
				if (hitEffect)
				{
					Main.npc[npcTarget].HitEffect(0, 1.0);
				}
			}
			else
			{
				Projectile.Kill();
			}
		}

		private void UpdateDrawLayer()
		{
			if (IsStickingToTarget)
			{
				int npcIndex = TargetWhoAmI;
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					return;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			Vector2 usePos = Projectile.position;

			Vector2 rotationVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
			usePos += rotationVector * 16f;

			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
				dust.position = (dust.position + Projectile.Center) / 2f;
				dust.velocity += rotationVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotationVector * 8f;
			}

			if (Projectile.owner == Main.myPlayer)
			{
				int item = 0;
				if (Main.rand.NextBool(18))
				{
					item = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<Items.Weapons.AbyssalFlag>());
				}

				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
		}

		private const int MaxStickingJavelin = 6;
		private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin];

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			IsStickingToTarget = true;
			TargetWhoAmI = target.whoAmI;
			Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
			Projectile.netUpdate = true;
			Projectile.damage = 0;

			Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingJavelins);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = height = 10;
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return projHitbox.Intersects(targetHitbox);
		}

		private const int AlphaFadeInSpeed = 25;

		private void UpdateAlpha()
		{
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= AlphaFadeInSpeed;
			}

			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
		}
	}
}