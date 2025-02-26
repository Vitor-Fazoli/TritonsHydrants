using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TritonsHydrants.Content.Buffs;

namespace TritonsHydrants.Common.GlobalItems;

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

    public override void HoldItem(Item item, Player player)
    {
        player.AddBuff(ModContent.BuffType<WaterAffinity>(), 2);
    }
}
