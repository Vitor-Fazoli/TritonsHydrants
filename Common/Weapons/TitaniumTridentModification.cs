using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common.Weapons
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
        private int portalQuantity;
        public override bool InstancePerEntity => true;
        public override void Load()
        {
            portalQuantity = 2;
        }
        public override void HoldItem(Item item, Player player)
        {
            for (int i = 0; i < portalQuantity; i++)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), player.Center, Vector2.Zero, ModContent.ProjectileType<IceOrb>(), 5 + player.statDefense, 20, player.whoAmI);
            }
        }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.TitaniumTrident;
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 10;
        }
    }
}
