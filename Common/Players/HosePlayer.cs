using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Common.Players
{
    public class HosePlayer : ModPlayer
    {
        public float WaterDamageMultiplier = 1f;

        public override void ResetEffects()
        {
            WaterDamageMultiplier = 1f;
        }
    }
}