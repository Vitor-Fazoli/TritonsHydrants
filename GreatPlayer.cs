using GreatswordsMod.Abstract;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Buffs;
using Terraria.DataStructures;

namespace GreatswordsMod
{
    public class GreatPlayer : ModPlayer
    {
        public int slayerPower = 0;

        public override void PreUpdateBuffs()
        {
            if((Main.LocalPlayer.HeldItem.ModItem is ItemGreatsword) && !Player.dead)
            {

                if (slayerPower == 1)
                {
                    Player.AddBuff(ModContent.BuffType<slayerPower1>(), 2);
                }
                else if (slayerPower == 2)
                {
                    Player.AddBuff(ModContent.BuffType<slayerPower2>(), 2);
                }
                else if (slayerPower == 3)
                {
                    Player.AddBuff(ModContent.BuffType<slayerPower3>(), 3);
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
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            Player.ClearBuff(ModContent.BuffType<slayerPower1>());
            Player.ClearBuff(ModContent.BuffType<slayerPower2>());
            Player.ClearBuff(ModContent.BuffType<slayerPower3>());
        }

    }
}
