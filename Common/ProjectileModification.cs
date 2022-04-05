using GearonArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Common.GlobalItems {
	
	public class TridentModification : GlobalProjectile {

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) {
			return entity.type == ProjectileID.Trident;
        }
        public override void SetDefaults(Projectile projectile) {
			projectile.DamageType = DamageClass.Magic;
        }
        public override void Kill(Projectile projectile, int timeLeft) {
			Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(projectile.Center.X, projectile.Center.Y), projectile.velocity * 1.5f, ModContent.ProjectileType<MagicMissile>(), projectile.damage, projectile.knockBack, projectile.owner);
		}
	}

	public class TitaniumTridentModification : GlobalProjectile {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) {
			return entity.type == ProjectileID.TitaniumTrident;
        }
        public override void SetDefaults(Projectile projectile) {
            projectile.DamageType = DamageClass.Magic;
        }
        public override void Kill(Projectile projectile, int timeLeft) {
            Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(projectile.Center.X, projectile.Center.Y), projectile.velocity * 1.5f, ModContent.ProjectileType<MagicMissile>(), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}