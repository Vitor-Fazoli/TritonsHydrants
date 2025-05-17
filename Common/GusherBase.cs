using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TritonsHydrants.Common
{
    public abstract class GusherBase : ModItem
    {
        protected const int MIN_MANA_COST = 10;
        protected const int MIN_USE_TIME_ANIMATION = 30;

        protected virtual int ManaCost { get; set; } = 10;
        protected virtual int BurstDamage { get; set; } = 20;
        protected virtual int BurstKnockback { get; set; } = 5;
        protected virtual int BuffType { get; set; }

        public override void HoldItem(Player player)
        {
            if (player.HeldItem.backSlot > 0)
            {
                player.back = player.HeldItem.backSlot;
                player.cBack = 0;
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse is not 2)
                return true;

            player.AddBuff(Item.buffType, 2);

            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, 0, 0, Main.myPlayer);
            projectile.originalDamage = 0;

            projectile.ai[0] = BuffType;

            return false;
        }
    }
}
