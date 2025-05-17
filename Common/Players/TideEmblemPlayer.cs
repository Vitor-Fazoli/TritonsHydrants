using log4net.Core;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Common.Players;

public class TideEmblemPlayer : ModPlayer
{
    public readonly int timerMax = Helper.Ticks(30);
    public int timer = Helper.Ticks(29);
    public bool isActive = false;
    public bool HighTide = false;
    public bool LowTide = false;


    public override void UpdateDead()
    {
        isActive = false;
    }

    override public void ResetEffects()
    {
        isActive = false;
    }

    public override void PreUpdate()
    {
        if (isActive)
        {
            timer++;

            if (timer >= timerMax)
            {
                ToggleTide();
                timer = 0;
            }


            if (HighTide)
            {
                Player.AddBuff(ModContent.BuffType<Content.Buffs.HighTide>(), 2);
            }
            else if (LowTide)
            {
                Player.AddBuff(ModContent.BuffType<Content.Buffs.LowTide>(), 2);
            }
        }
    }

    public void ToggleTide()
    {
        if (HighTide)
        {
            HighTide = false;
            LowTide = true;
        }
        else if (LowTide)
        {
            LowTide = false;
            HighTide = true;
        }
        else
        {
            HighTide = true;
        }
    }

}