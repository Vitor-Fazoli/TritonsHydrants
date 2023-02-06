using DevilsWarehouse.Common.Systems;
using DevilsWarehouse.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    [AutoloadEquip(EquipType.Back)]
    public class AncientScroll : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("With great scrolls comes great responsibility\n" +
                "when you crit, a scroll uses your power\n" +
                "increase throwing damage\n" +
                "increase critical strike chance");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Throwing) += 0.1f;


            player.GetModPlayer<AncientScrollPlayer>().ancientScroll = true;
            player.AddBuff(ModContent.BuffType<NinjaScrollBuff>(), 2);

            if (player.ownedProjectileCounts[ModContent.ProjectileType<NinjaScroll>()] <= 0)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), player.position, Vector2.Zero, ModContent.ProjectileType<NinjaScroll>(), 10, 5, player.whoAmI);
            }
        }
    }
    public class AncientScrollPlayer : ModPlayer
    {
        public bool ancientScroll = false;
        public bool critical;

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ancientScroll)
            {
                if (crit)
                {
                    critical = true;
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (ancientScroll)
            {
                if (crit)
                {
                    critical = true;
                }
            }
        }
    }
}
