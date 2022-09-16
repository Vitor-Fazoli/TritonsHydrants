using Terraria;
using Terraria.ModLoader;


namespace VoidArsenal.Content.Buffs
{
    public class KingsBlessing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kings's Blessing");
            Description.SetDefault("Melee Damage increased \ngains defense if Queen's blessing active");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) += 0.1f;

            if (player.HasBuff(ModContent.BuffType<QueensBlessing>()))
            {
                player.statDefense += 10;
            }
        }
    }
}
