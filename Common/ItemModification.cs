using VoidArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VoidArsenal.Common.Abstract;

namespace VoidArsenal.Common
{
    internal class TridentItemModification : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Trident;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 10;
        }
    }
    internal class TitaniumTridentItemModification : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.TitaniumTrident;
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 30;
        }
    }
}
