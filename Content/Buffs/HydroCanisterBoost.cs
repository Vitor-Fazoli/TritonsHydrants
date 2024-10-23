using Terraria;
using Terraria.ModLoader;
namespace TritonsHydrants.Content.Buffs
{
    public class HydroCanisterBoost : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<HydroCanisterBoosterPlayer>().IsConsumed = false;
            
            player.GetDamage(DamageClass.Ranged) += 0.10f; // 10%++
            player.GetAttackSpeed(DamageClass.Melee) += 0.5f; // 50%++
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