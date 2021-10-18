using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Buffs
{
    public class WarriorFlame : ModBuff
    {
        public override void SetStaticDefaults(){

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Warrior Flame");
            Description.SetDefault("");
        }

        public override void Update(Player player, ref int buffIndex){
            
            player.thorns += 10;

            effectBuff(player);
        }
        public void effectBuff(Entity target){

            int num1 = Dust.NewDust(target.position, target.width, target.height, DustID.FlameBurst);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }
    }
}