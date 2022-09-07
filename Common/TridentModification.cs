//using VoidArsenal.Content.Projectiles;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace VoidArsenal.Common.GlobalItems
//{
//    public class TridentProjectile : GlobalProjectile
//    {

//        private bool canShoot = true;

//        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
//        {
//            return entity.type == ProjectileID.Trident;
//        }
//        public override void SetDefaults(Projectile projectile)
//        {
//            projectile.DamageType = DamageClass.Magic;
//        }
//        public override void OnSpawn(Projectile projectile, IEntitySource source)
//        {
//            canShoot = true;
//        }
//        public override void AI(Projectile projectile)
//        {
//            if (projectile.timeLeft <= projectile.timeLeft / 3 && canShoot)
//            {
//                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(projectile.Center.X, projectile.Center.Y), projectile.velocity * 1.5f, ModContent.ProjectileType<AquaticArrow>(), projectile.damage, projectile.knockBack, projectile.owner);
//                canShoot = false;
//            }
//        }
//    }
//    public class TridentItem : GlobalItem
//    {
//        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
//        {
//            return entity.type == ItemID.Trident;
//        }
//        public override void SetDefaults(Item item)
//        {
//            item.DamageType = DamageClass.Magic;
//            item.mana = 10;
//        }
//    }
//}