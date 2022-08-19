using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VoidArsenal.Content.Projectiles
{
    public class BeeDart : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Dart");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 10; 
			Projectile.height = 6;
			Projectile.aiStyle = 1; 
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600; 
			Projectile.alpha = 255; 
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;

			AIType = ProjectileID.Bullet;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Projectile.NewProjectile(new EntitySource_OnHit(target, target),Main.player[Projectile.owner].position + Main.rand.NextVector2Circular(10f,10f), Projectile.velocity / 5, ProjectileID.Bee, 2, 0, Projectile.owner);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}

        public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}
