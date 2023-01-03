using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common
{
    #region Weapons
    internal class ThrowingItems : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type switch
            {
                ItemID.BoneDagger => true,
                ItemID.PaladinsHammer => true,
                ItemID.BoneGlove => true,
                ItemID.Shuriken => true,
                ItemID.ThrowingKnife => true,
                ItemID.AleThrowingGlove => true,
                ItemID.Snowball => true,
                ItemID.RottenEgg => true,
                ItemID.PoisonedKnife => true,
                ItemID.Beenade => true,
                ItemID.StarAnise => true,
                ItemID.SpikyBall => true,
                ItemID.Javelin => true,
                ItemID.FrostDaggerfish => true,
                ItemID.MolotovCocktail => true,
                ItemID.BoneJavelin => true,
                ItemID.Grenade => true,
                ItemID.BouncyGrenade => true,
                ItemID.PartyGirlGrenade => true,
                ItemID.StickyGrenade => true,
                _ => false,
            };
        }

        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class ThrowingProjectiles : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type switch
            {
                ProjectileID.BoneDagger => true,
                ProjectileID.PaladinsHammerFriendly => true,
                ProjectileID.BoneGloveProj => true,
                ProjectileID.Shuriken => true,
                ProjectileID.ThrowingKnife => true,
                ProjectileID.Ale => true,
                ProjectileID.SnowBallFriendly => true,
                ProjectileID.RottenEgg => true,
                ProjectileID.PoisonedKnife => true,
                ProjectileID.Beenade => true,
                ProjectileID.StarAnise => true,
                ProjectileID.SpikyBall => true,
                ProjectileID.JavelinFriendly => true,
                ProjectileID.FrostDaggerfish => true,
                ProjectileID.MolotovCocktail => true,
                ProjectileID.BoneJavelin => true,
                ProjectileID.Grenade => true,
                ProjectileID.BouncyGrenade => true,
                ProjectileID.PartyGirlGrenade => true,
                ProjectileID.StickyGrenade => true,
                _ => false,
            };
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.DamageType = DamageClass.Throwing;
        }
    }
    #endregion

    #region Armors
    public class NinjaShirt : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NinjaShirt;
        }
        public override void UpdateArmorSet(Player player, string set)
        {
            player.GetDamage(DamageClass.Throwing) += 0.05f;
        }
        public override void SetDefaults(Item item)
        {
            item.defense = 4;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.1f;
        }
    }
    public class NinjaHood : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NinjaHood;
        }
        public override void SetDefaults(Item item)
        {
            item.defense = 2;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            player.GetAttackSpeed(DamageClass.Throwing) += 0.15f;
        }
    }
    public class NinjaPants : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NinjaPants;
        }
        public override void SetDefaults(Item item)
        {
            item.defense = 3;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            player.GetCritChance(DamageClass.Throwing) += 0.1f;
            Mod.Logger.Warn("chegou aqui");
        }
    }
    #endregion
}
