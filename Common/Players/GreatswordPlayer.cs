using GearonArsenalMod.Abstract;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using GearonArsenalMod.Content.Buffs;

namespace GearonArsenalMod.Common.Players{ 

    public class GreatswordPlayer : ModPlayer{
        //All of greatswords things
        public int slayerPower = 0;
        public int slayerMax = 3;
        public bool lantern = false;
        public bool balm = false;


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
        public override void ResetEffects()
        {
            lantern = false;
            balm = false;
        }

        public override void UpdateEquips()
        {
            if (lantern)
            {
                Player.GetModPlayer<GreatswordPlayer>().slayerMax = 5;
            }
            else if (balm)
            {
                Player.GetModPlayer<GreatswordPlayer>().slayerMax = 2;
            }
            else
            {
                Player.GetModPlayer<GreatswordPlayer>().slayerMax = 3;
            }
        }
    }
}
