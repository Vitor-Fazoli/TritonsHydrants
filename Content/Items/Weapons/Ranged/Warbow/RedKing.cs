using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using VoidArsenal.Content.Projectiles;
using System.Collections.Generic;

namespace VoidArsenal.Content.Items.Weapons.Ranged.Warbow
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
            TooltipLine synergy = new TooltipLine(Mod,"Synergy: ","every time you hit an enemy with the Red Queen, your next shot deals more damage to that target") { OverrideColor = Color.Red };
            tooltips.Add(synergy);
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LastPrism);
            Item.damage = 42;
            Item.shoot = ModContent.ProjectileType<Projectiles.RedKing>();
            Item.shootSpeed = 30f;
            Item.DamageType = DamageClass.Ranged;
            Item.mana = 0;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.ammo = AmmoID.Arrow;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.RedKing>()] <= 0 && player.HasItem(ItemID.WoodenArrow);
    }
}