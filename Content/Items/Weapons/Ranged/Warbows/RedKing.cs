using DevilsWarehouse.Content.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Weapons.Ranged.Warbows
{
    public class RedKing : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red King");
            Tooltip.SetDefault(@"");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine synergy = new TooltipLine(Mod, "Synergy: ", "every time you hit an enemy with the Red Queen, your next shot deals more damage to that target") { OverrideColor = Color.IndianRed };
            tooltips.Add(synergy);
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 80;
            Item.noMelee = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.RedKing>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            player.AddBuff(ModContent.BuffType<RedKingHeart>(), 180);
        }
        public override Vector2? HoldoutOffset()
        {
            return new(-12 * Item.direction, 0);
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.RedKing>()] <= 0;
    }
}