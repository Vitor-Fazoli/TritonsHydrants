using GearonArsenalMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.Buffs
{
    public class WarriorWraith : ModBuff
    {
        public override void SetStaticDefaults(){

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Warrior Wraith");
            Description.SetDefault("Attackers also suffer part of damage\n" +
                "Increase defense by 10");
        }
        public override void Update(Player player, ref int buffIndex){
            player.thorns += 0.1f;
            player.statDefense += 10;
            effectBuff(player);
        }
        public void effectBuff(Entity target){

            int num1 = Dust.NewDust(target.position, target.width, target.height, DustID.RedTorch);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }
    }
}