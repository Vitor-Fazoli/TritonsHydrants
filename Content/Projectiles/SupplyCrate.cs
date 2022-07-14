using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VoidArsenal.Content.Buffs;

namespace VoidArsenal.Content.Projectiles
{
    public class SupplyCrate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supply Crate");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
        }

        public override bool OnPickup(Player player)
        {
            int percent = 10 * (player.statLifeMax2 / 100);

            int bonus = player.statManaMax2 / 2;

            if (player.statLife <= player.statLifeMax2)
            {
                player.statLife += (percent + bonus)/4;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), CombatText.HealLife, (percent + bonus)/4);
            }

            switch (Main.rand.Next(2))
            {
                case 0:
                    player.AddBuff(ModContent.BuffType<Foward>(), 300);
                    break;
                case 1:
                    player.AddBuff(ModContent.BuffType<Retreat>(), 300);
                    break;
                case 2:
                    player.AddBuff(ModContent.BuffType<Standing>(), 300);
                    break;
            }

            Item.active = false;
            return false;
        }
    }
}
