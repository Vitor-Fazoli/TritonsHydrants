using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Buffs
{
    public class QueensBlessing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen's Blessing");
            Description.SetDefault("Ranged Damage increased \ngains movement speed if King's blessing active");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) += 0.1f;

            if (player.HasBuff(ModContent.BuffType<QueensBlessing>()))
            {
                player.moveSpeed += 0.2f;
            }
        }
    }
}
