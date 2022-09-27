using Terraria;
using Terraria.ModLoader;


namespace VoidArsenal.Content.Buffs
{
    public class RedKingHeart : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kings's Blessing");
            Description.SetDefault("Melee Damage increased \ngains defense if Queen's blessing active");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            int queen = ModContent.BuffType<RedQueenHeart>();

            player.GetDamage(DamageClass.Ranged) += 0.1f;

            if (player.HasBuff(ModContent.BuffType<RedQueenHeart>()))
            {
               
                player.statDefense += 10;

                int locationRedQueen = player.FindBuffIndex(queen);

                if (player.buffType[locationRedQueen] > buffIndex)
                {
                    buffIndex = player.buffType.Length - 1;
                    player.ClearBuff(queen);
                    player.AddBuff(queen, Readability.toSeconds(60));
                }
            }

            
        }
    }
}
