using DevilsWarehouse.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class WarriorWraith : ModBuff
    {
        public override void SetStaticDefaults(){

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Warrior Wraith");
            Description.SetDefault("Attackers also suffer part of damage\n" +
                "Increase defense");
        }
        public override void Update(Player player, ref int buffIndex){
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();

            player.thorns += 0.1f + (0.2f * 1 / modPlayer.slayerMax);
            player.statDefense += 10 + (40 * 1 / modPlayer.slayerMax);
            Effect(player);
        }
        public static void Effect(Entity target){

            int num1 = Dust.NewDust(target.position, target.width, target.height, DustID.RedTorch);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }
    }
}