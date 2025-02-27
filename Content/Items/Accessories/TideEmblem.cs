using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Items.Accessories
{
    public class TideEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TideEmblemP>().isActive = true;
        }
    }

    public class TideEmblemP : ModPlayer
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
                    Player.AddBuff(ModContent.BuffType<Buffs.HighTide>(), 2);
                }
                else if (LowTide)
                {
                    Player.AddBuff(ModContent.BuffType<Buffs.LowTide>(), 2);
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
}