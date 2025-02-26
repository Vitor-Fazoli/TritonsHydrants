using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Buffs;

public class MindExplosion : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<MindExplosionPlayer>().mindExplosion = true;
    }
}

public class MindExplosionPlayer : ModPlayer
{
    public bool mindExplosion;

    public override void ResetEffects()
    {
        mindExplosion = false;
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!mindExplosion || item.DamageType != DamageClass.Magic)
            return;

        mindExplosion = false;
        hit.Damage *= 2;
        target.StrikeNPC(hit);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!mindExplosion || proj.DamageType != DamageClass.Magic)
            return;

        mindExplosion = false;
        hit.Damage *= 2;
        target.StrikeNPC(hit);
    }
}