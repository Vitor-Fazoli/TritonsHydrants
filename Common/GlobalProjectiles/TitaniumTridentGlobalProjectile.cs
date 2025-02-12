using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Common.GlobalProjectiles;

public class TitaniumTridentProjectile : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.TitaniumTrident;
    public override void SetDefaults(Projectile projectile)
    {
        projectile.DamageType = DamageClass.Magic;
    }
    public override void AI(Projectile projectile)
    {
        if (projectile.timeLeft <= projectile.timeLeft / 3)
        {
            Projectile.NewProjectile
            (
                new EntitySource_TileBreak(2, 2),
                new Vector2(projectile.Center.X, projectile.Center.Y),
                projectile.velocity * 1.5f,
                ModContent.ProjectileType<AquaticArrow>(),
                projectile.damage,
                projectile.knockBack,
                projectile.owner
            );
        }
    }
}