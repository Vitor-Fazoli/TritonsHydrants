using GearonArsenalMod.Abstract;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace GearonArsenalMod.Common.Players{ 

    public class GreatswordPlayer : ModPlayer{

        public int slayerPower = 0;
        public int slayerMax = 3;

        public override void PreUpdateBuffs(){

            if((Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword) && !Player.dead){

                if (slayerPower == 1){

                    Player.GetCritChance(DamageClass.Melee) += 5;
                    Player.meleeSpeed += 0.10f;
                }
                else if (slayerPower == 2){

                   Player.GetCritChance(DamageClass.Melee) += 10;
                   Player.meleeSpeed += 0.15f;
                }
                else if (slayerPower == 3){

                    Player.GetCritChance(DamageClass.Melee) += 20;
                    Player.meleeSpeed += 0.30f;
                }

                if (slayerPower > slayerMax){

                    slayerPower = slayerMax;
                }
            }
            else{

                slayerPower = 0;
            }
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource){

            Player.ClearBuff(ModContent.BuffType<CursedSkull>());
        }
    }
}
