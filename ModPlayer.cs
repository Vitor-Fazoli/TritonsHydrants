using GearonArsenalMod.Abstract;
using Terraria;
using Terraria.ModLoader;
using GearonArsenalMod.Buffs;
using Terraria.DataStructures;

namespace GearonArsenalMod
{
    public class ModPlayer : Terraria.ModLoader.ModPlayer
    {
        public int slayerPower = 0;
        public float speedCDR;
        public override void PreUpdateBuffs()
        {
            if((Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword) && !Player.dead)
            {
                if (slayerPower == 1)
                {
                    Player.AddBuff(ModContent.BuffType<SlayerPowerOne>(), 2);
                }
                else if (slayerPower == 2)
                {
                    Player.AddBuff(ModContent.BuffType<SlayerPowerTwo>(), 2);
                }
                else if (slayerPower == 3)
                {
                    Player.AddBuff(ModContent.BuffType<SlayerPowerThree>(), 3);
                }

                if (slayerPower >= 3)
                {
                    slayerPower = 3;
                }
            }
            else
            {
                slayerPower = 0;
            }
        }
        public override void PostUpdateEquips()
        {
            speedCDR = Player.meleeSpeed * 50;
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            Player.ClearBuff(ModContent.BuffType<SlayerPowerOne>());
            Player.ClearBuff(ModContent.BuffType<SlayerPowerTwo>());
            Player.ClearBuff(ModContent.BuffType<SlayerPowerThree>());
        }

    }
}
