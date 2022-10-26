using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles{

	public class WarriorWraithProj : ModProjectile{

		public override void SetStaticDefaults(){

			DisplayName.SetDefault("Warrior Wraith");
			Main.projFrames[Projectile.type] = 15;
		}

		public override void SetDefaults(){

			Projectile.width = 68;
			Projectile.height = 76;
			Projectile.damage = 0;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Melee;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
			hitbox.Width = 0;
			hitbox.Height = 0;
        }
        public override void AI(){

			Player p = Main.player[Projectile.owner];
			Projectile.position.Y = p.position.Y - 65;
			Projectile.position.X = p.Center.X - 35;
			Projectile.spriteDirection = p.direction;

            #region Animation
            if (++Projectile.frameCounter >= 3){
				
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
				{

					Projectile.frame = 0;
					Projectile.Kill();
				}
			}
            #endregion
        }
    }
}