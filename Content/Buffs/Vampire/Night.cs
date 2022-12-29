using Microsoft.Xna.Framework;
using System.Drawing;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs.Vampire
{
    public class Night : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power of Moon");
            Description.SetDefault("i'm feeling good\n" +
                "improves vision\n" +
                "increased your damage and speed");
        }
        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player Player, ref int buffIndex)
        {
            Player.GetModPlayer<MoonBuffPlayer>().moonBuff = true;
        }
    }
    public class MoonBuffPlayer : ModPlayer
    {
        public bool moonBuff;

        public override void ResetEffects()
        {
            moonBuff = false;
        }
        public override void UpdateDead()
        {
            moonBuff = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (moonBuff)
            {
                Player.moveSpeed += 0.3f;

                Player.GetDamage(DamageClass.Generic) += 0.2f;

                Player.nightVision = true;
            }
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            Lighting.AddLight(Player.position, 0.5f, 0.5f, 0.5f);
        }
    }
}