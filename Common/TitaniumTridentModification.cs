using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using VoidArsenal.Content.Projectiles;

namespace VoidArsenal.Common
{
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
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(projectile.Center.X, projectile.Center.Y), projectile.velocity * 1.5f, ModContent.ProjectileType<AquaticArrow>(), projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
    }
    public class TitaniumTridentItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.TitaniumTrident;
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 10;
        }
    }
}
