using Microsoft.Xna.Framework;
using System.Threading;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs.Vampire
{
    public class Day : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power of sun");
            Description.SetDefault("it's burning!\n" +
                "decreased your defense and receive damage per second but increase your damage");
        }
        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DayPlayer>().Day = true;
        }
    }

    public class DayPlayer : ModPlayer
    {
        public bool Day;

        public override void ResetEffects()
        {
            Day = false;
        }
        public override void UpdateDead()
        {
            Day = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (Day)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 2;

                Player.statDefense *= (int)0.8;

                Player.GetDamage(DamageClass.Generic) += 01f;
            }

        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Day)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Torch, 0, -1.86f, 100, Color.White, 1.2f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }
    }
}