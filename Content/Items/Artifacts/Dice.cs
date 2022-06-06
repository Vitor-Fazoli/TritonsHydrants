using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using GearonArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;

namespace GearonArsenal.Content.Items.Artifacts
{
    internal class Dice : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("each critical damage you throw a card in a enemy, if the three types of card hit a same enemy you killed instantly");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<Artifact>();
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<LuckyMan>().luckyMan = true;
        }
    }
    public class LuckyMan : ModPlayer
    {
        public bool luckyMan = false;

        public int manaCost = 50;
        public int damage = 20;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Ranged && luckyMan && crit)
            {
                if (Player.statMana >= manaCost)
                {
                    Player.statMana -= manaCost;

                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X + 20, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SuppliesCrate>(), 1);

                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X - 20, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SuppliesCrate>(), 1);

                    //Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(Player.Center.X, Player.Center.Y - 10), new Vector2(
                    //    0, -5), ModContent.ProjectileType<SuppliesCrate>(), damage, 5, proj.owner);
                }
            }
        }
    }
}
