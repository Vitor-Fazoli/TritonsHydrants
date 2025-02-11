using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Buffs
{
    public class KnowledgePower : ModBuff
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
            player.GetModPlayer<BookOfKnowledgePlayer>().knowledgePower = true;
        }
    }

    public class BookOfKnowledgePlayer : ModPlayer
    {
        public bool knowledgePower;

        public override void ResetEffects()
        {
            knowledgePower = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!knowledgePower || item.DamageType != DamageClass.Magic)
                return;

            knowledgePower = false;
            hit.Damage *= 2;
            target.StrikeNPC(hit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!knowledgePower || proj.DamageType != DamageClass.Magic)
                return;

            knowledgePower = false;
            hit.Damage *= 2;
            target.StrikeNPC(hit);
        }
    }
}