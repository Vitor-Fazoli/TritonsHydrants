using GearonArsenalMod.Common.Players;
using GearonArsenalMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod.Content.Buffs
{
    public class LampionFlames : ModBuff
    {
        public bool equip;
        public  override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Lampiom Flames");
            Description.SetDefault("Increased Fury Counter");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();
            modPlayer.Lampion = true;
            if(!equip)
                modPlayer.slayerMax += 2;
            equip = true;
        }
    }
}
