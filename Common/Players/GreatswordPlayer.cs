using GearonArsenalMod.Abstract;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using GearonArsenalMod.Content.Buffs;

namespace GearonArsenalMod.Common.Players{ 

    public class GreatswordPlayer : ModPlayer{

        public int slayerPower = 0;
        public int slayerMax = 3;
        public bool Lampion;

        public override void PreUpdateBuffs(){

            if((Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword) && !Player.dead){

                Player.GetCritChance(DamageClass.Melee) += 5 * slayerPower;
                Player.meleeSpeed += 0.05f * slayerPower;

                if (slayerPower >= slayerMax){

                    slayerPower = slayerMax;
                }
            }
            else{

                slayerPower = 0;
            }
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource){

            Player.ClearBuff(ModContent.BuffType<WarriorWraith>());
            Player.ClearBuff(ModContent.BuffType<LampionFlames>());
        }
        public override void ResetEffects()
        {
            Lampion = false;
        }
        public override void UpdateEquips()
        {

        }
    }
}
