using GearonArsenalMod.Abstract;
using Terraria;
using Terraria.ModLoader;
using GearonArsenalMod.Buffs;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace GearonArsenalMod{

    public class ModPlayer : Terraria.ModLoader.ModPlayer{

        public int slayerPower = 0;
        public int slayerMax = 3;
        public float speedCDR;

        public override void PreUpdateBuffs()
        {
            if((Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword) && !Player.dead){

                if (slayerPower == 1){

                    Player.GetCritChance(DamageClass.Melee) += 8;
                }
                else if (slayerPower == 2){

                   Player.GetCritChance(DamageClass.Melee) += 16;
                }
                else if (slayerPower == 3){

                    Player.GetCritChance(DamageClass.Melee) += 32;
                }

                if (slayerPower > slayerMax){

                    slayerPower = slayerMax;
                }
            }
            else{

                slayerPower = 0;
            }
        }
        public override void PostUpdateEquips(){

            speedCDR = Player.meleeSpeed * 50;
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource){

            Player.ClearBuff(ModContent.BuffType<CursedSkull>());
        }
    }
}
