using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Common.Players
{
    public class HydrantPlayer : ModPlayer
    {
        // ESTILOS DE BUFF DOS HIDRANTES

        public enum HydrantStyle
        {
            Refreshed,
            Refueled,
            
        }


        public bool isRefreshed = false;
        public bool isRefueled = false;

        public override void ResetEffects()
        {
            isRefreshed = false;
            isRefueled = false;
        }

        public override void UpdateDead()
        {
            isRefreshed = false;
            isRefueled = false;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (isRefreshed)
            {
                int damageReceived = hurtInfo.Damage / 5;

                Player.HealEffect(damageReceived, true);
                Player.Heal(damageReceived);
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (isRefreshed)
            {

                int damageReceived = hurtInfo.Damage / 5;

                Player.HealEffect(damageReceived, true);
                Player.Heal(damageReceived);
            }
        }

        public override bool CanUseItem(Item item)
        {
            if (isRefueled)
            {
                if (item.type == ModContent.ItemType<HoseBase>())
                {
                    Player.ammoCost80 = true;
                }
            }

            return base.CanUseItem(item);
        }
    }
}