using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class Night : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power of Moon");
            Description.SetDefault("i'm feeling good");
        }
        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MoonBuffPlayer>().moonBuff = true;
        }
    }
    public class MoonBuffPlayer : ModPlayer
    {
        public bool moonBuff;

        public override void ResetEffects()
        {
            moonBuff = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (moonBuff)
            {
                Player.moveSpeed += 0.3f;

                Player.GetDamage(DamageClass.Generic) += 0.2f;
            }


        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (moonBuff)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.IceTorch, 0, -1.86f, 100,Color.White, 1.2f);
                    Main.dust[dust].noGravity = true;
                }
                Lighting.AddLight(Player.position, 0.1f, 0.2f, 0.7f);
            }
        }
    }
}