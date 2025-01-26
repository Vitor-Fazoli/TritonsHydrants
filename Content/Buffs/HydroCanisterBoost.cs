using Terraria;
using Terraria.ModLoader;
namespace TritonsHydrants.Content.Buffs
{
    public class HydroCanisterBoost : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<HydroCanisterBoosterPlayer>().IsConsumed = false;
            player.GetAttackSpeed(DamageClass.Ranged) += 2.0f;
        }
    }

    public class HydroCanisterBoosterPlayer : ModPlayer
    {
        public bool IsConsumed;

        public override void ResetEffects()
        {
            IsConsumed = true;
        }
    }
}