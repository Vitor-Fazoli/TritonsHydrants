﻿using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using TritonsHydrants.Content.Buffs;

namespace TritonsHydrants.Common.Systems
{
    public abstract class BaseTridentItem : ModItem
    {
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<WaterAffinity>(), 2);
        }
    }
}
