using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using MagicTridents.Content.Projectiles;

namespace MagicTridents.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class TridentProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.Trident;
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

    /// <summary>
    /// 
    /// </summary>
    public class TridentItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Trident;
        }

        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 10;
            item.UseSound = SoundID.Item71;

            item.StatsModifiedBy.Add(Mod);
        }
    }
}
