using GreatswordsMod.Abstract;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Buffs;
<<<<<<< HEAD
using Terraria.DataStructures;
=======
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99

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
<<<<<<< HEAD
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            Player.ClearBuff(ModContent.BuffType<slayerPower1>());
            Player.ClearBuff(ModContent.BuffType<slayerPower2>());
            Player.ClearBuff(ModContent.BuffType<slayerPower3>());
        }

    }
=======

		public static readonly int DashRight = -1;
		public static readonly int DashLeft = 1;

		public int DashDir = -1;

		public bool DashActive = false;
		public int DashDelay = MAX_DASH_DELAY;
		public int DashTimer = MAX_DASH_TIMER;

		public readonly float DashVelocity = 10f;

		public static readonly int MAX_DASH_DELAY = 50;
		public static readonly int MAX_DASH_TIMER = 35;

		public override void ResetEffects()
		{
			bool dashAccessoryEquipped = false;

			for (int i = 3; i < 8; i++)
			{

			}

			if (!dashAccessoryEquipped || Player.setSolar || Player.mount.Active || DashActive)
				return;

			DashActive = true;
		}

	}
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99
}
