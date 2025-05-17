using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Common.Players
{
    public class HydrantPlayer : ModPlayer
    {
        public bool isRefreshed = false; // Refrescado
        public bool isRefueled = false; // Reabastecido
        public bool isLithe = false; // Agil
        public bool isFlooded = false; // Inundado

        public override void ResetEffects()
        {
            isRefreshed = false;
            isRefueled = false;
            isLithe = false;
            isFlooded = false;
        }

        public override void UpdateDead()
        {
            ResetEffects();
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (isRefreshed)
            {
                int damageReceived = (int)(hurtInfo.Damage * Helper.Percentage(20));

                Player.HealEffect(damageReceived, true);
                Player.Heal(damageReceived);
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (isRefreshed)
            {
                int damageReceived = (int)(hurtInfo.Damage * Helper.Percentage(20));

                Player.HealEffect(damageReceived, true);
                Player.Heal(damageReceived);
            }
        }

        public override bool CanUseItem(Item item)
        {
            if (isRefueled)
            {
                if (item.type == ModContent.ItemType<GusherBase>())
                {
                    Player.ammoCost80 = true;
                }
            }

            return base.CanUseItem(item);
        }
    }
}