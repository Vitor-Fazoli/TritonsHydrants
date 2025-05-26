using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Buffs;

public class Refreshed : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        var p = player.GetModPlayer<RefreshedPlayer>();

        player.GetAttackSpeed(DamageClass.Summon) += TritonsHelper.Percentage(10);
        p.isRefreshed = true;
    }
}

internal class RefreshedPlayer : ModPlayer
{
    public bool isRefreshed;

    public override void ResetEffects()
    {
        isRefreshed = false;
    }

    public override void UpdateDead()
    {
        ResetEffects();
    }
    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        if (isRefreshed)
        {
            int fractionalDamage = (int)(hurtInfo.Damage * TritonsHelper.Percentage(20));

            if (fractionalDamage > 0)
                Player.Heal(fractionalDamage);
        }
    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        if (isRefreshed)
        {
            int fractionalDamage = (int)(hurtInfo.Damage * TritonsHelper.Percentage(20));

            if (fractionalDamage > 0)
                Player.Heal(fractionalDamage);
        }
    }
}