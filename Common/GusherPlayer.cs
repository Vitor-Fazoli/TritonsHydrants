using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Common
{
    public class GusherPlayer : ModPlayer
    {
        public int ChargeTimer = 0;
        public int MaxCharge = 90;
        public bool IsCharging = false;
        public bool JustReleased = false;

        public float ChargeProgress => MathHelper.Clamp(ChargeTimer / (float)MaxCharge, 0f, 1f);

        public override void PreUpdate()
        {
            // Reseta JustReleased todo tick — é consumido no Shoot
            JustReleased = false;

            if (Player.HeldItem.ModItem is not GusherBase)
            {
                ChargeTimer = 0;
                IsCharging = false;
                return;
            }

            if (Player.altFunctionUse is 2)
            {
                ChargeTimer = 0;
                IsCharging = false;
                return;
            }

            bool pressing = Player.controlUseItem && !Player.mouseInterface;

            if (pressing)
            {
                IsCharging = true;
                if (ChargeTimer < MaxCharge)
                    ChargeTimer++;

                // Mantém a animação rodando sem travar o jogador
                // useAnimation/2 garante que a arma aponta pro cursor
                Player.itemAnimation = Player.HeldItem.useAnimation / 2;
                Player.itemTime = Player.HeldItem.useTime / 2;
            }
            else if (IsCharging)
            {
                // Soltou — sinaliza para disparar
                IsCharging = false;
                JustReleased = true;

                // Deixa a animação finalizar normalmente
                Player.itemAnimation = 2;
                Player.itemTime = 2;
            }
            else
            {
                ChargeTimer = 0;
            }
        }
    }
}