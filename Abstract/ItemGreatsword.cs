using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using System.Text;
using GearonArsenalMod.Common.Players;
using GearonArsenalMod.Content.Item.Weapons;
using GearonArsenalMod.Content.Projectiles;

namespace GearonArsenalMod.Abstract {
    public abstract class ItemGreatsword : ModItem {

        protected int greatsword = ModContent.ProjectileType<CopperGreatswordP>();
        protected Greatsword gStats = ModContent.GetInstance<CopperGreatswordP>();
        protected int slash = ModContent.ProjectileType<CopperSlash>();

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
            Item.crit = -4;
            Item.knockBack = 0;
            Item.shoot = greatsword;
            Item.defense = gStats.defense;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];

            foreach (TooltipLine line in tooltips) {
                if (line.mod.Equals("Terraria") && line.Name.Equals("Damage")) {

                    line.text = (int)(gStats.GetDmg() * 0.6 * player.GetDamage(DamageClass.Melee)) + " Light Melee Damage" +
                        "\n" + (int)(gStats.GetDmg() * 2.5 * player.GetDamage(DamageClass.Melee)) + " Heavy Melee Damage";
                }

                if (line.mod.Equals("Terraria") && line.Name.Equals("Speed"))
                    line.text = ((gStats.GetCooldown() * player.meleeSpeed) / 60).ToString("F2") + " Seconds to channel";

                if (line.mod.Equals("Terraria") && line.Name.Equals("Tooltip0"))
                    line.text = "Hold attack to do greater damage";
            }
        }

        public override void HoldItem(Player player) {
            player.statDefense += gStats.defense;
            player.aggro += gStats.aggro;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            position.Y = player.position.Y - 62;
        }
        public override bool AltFunctionUse(Player player) {
            return true;
        }
        public override bool CanUseItem(Player player) {
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();

            if (player.altFunctionUse == 2) {
                if (modPlayer.slayerPower >= modPlayer.slayerMax) {
                    Item.useStyle = ItemUseStyleID.HoldUp;
                    Item.useTime = 20;
                    Item.useAnimation = 20;
                    modPlayer.slayerPower = 0;
                    player.AddBuff(gStats.buff, 120 + (600 * 1 / modPlayer.slayerMax));
                    Item.shoot = gStats.buffVisual;
                    SoundEngine.PlaySound(26, player.position, 1);
                } else {
                    return false;
                }
            } else {
                Item.DamageType = DamageClass.Melee;
                Item.useTime = 50;
                Item.useAnimation = 50;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.noMelee = true;
                Item.channel = true;
                Item.crit = -4;
                Item.damage = 10;
                Item.knockBack = 0;
                Item.shoot = greatsword;
            }

            return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
                player.ownedProjectileCounts[greatsword] +
                player.ownedProjectileCounts[slash] < 1;
        }
    }
}