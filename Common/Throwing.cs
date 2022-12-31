using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common
{
    #region Weapons

    internal class BoneKnife : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.BoneDagger;
        }

        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class PaladinsHammer : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.PaladinsHammer;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class BoneGlove : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.BoneGlove;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class Shuriken : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Shuriken;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class Beenade : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Beenade;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class BouncyGrenade : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.BouncyGrenade;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class StickGrenade : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.StickyGrenade;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class Daybreak : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.DayBreak;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class PossessedHatchet : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.PossessedHatchet;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class PartyGirlGrenade : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.PartyGirlGrenade;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    internal class Grenade : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Grenade;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class Javelin : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Javelin;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class FrostDaggerFish : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.FrostDaggerfish;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }

    internal class BoneDagger : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.BoneDagger;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Throwing;
        }
    }
    #endregion

    #region Armors
    internal class NinjaShirt : GlobalItem
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
            item.material = true;
            item.defense = 3;
        }
    }
    internal class NinjaHood : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NinjaHood;
        }
        public override void UpdateArmorSet(Player player, string set)
        {
            player.GetDamage(DamageClass.Throwing) += 0.05f;
        }
        public override void SetDefaults(Item item)
        {
            item.material = true;
            item.defense = 3;
        }
    }
    internal class NinjaPants : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NinjaPants;
        }
        public override void UpdateArmorSet(Player player, string set)
        {
            player.GetDamage(DamageClass.Throwing) += 0.05f;
        }
        public override void SetDefaults(Item item)
        {
            item.material = true;
            item.defense = 3;
        }
    }
    #endregion

}
