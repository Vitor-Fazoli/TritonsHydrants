using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using DevilsWarehouse.Content.Items.Weapons.Melee.Greatswords;
using DevilsWarehouse.Content.Projectiles;

namespace DevilsWarehouse.Common.Abstract
{
    public abstract class ItemGreatsword : ModItem
    {
        protected int greatsword = ModContent.ProjectileType<CopperGreatswordP>();
        protected Greatsword gStats = ModContent.GetInstance<CopperGreatswordP>();
        protected int slash = ModContent.ProjectileType<CopperSlash>();

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = false;
            Item.crit = -4;

            Item.knockBack = gStats.GetKnk();
            Item.shoot = greatsword;
            Item.defense = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];

            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod.Equals("Terraria") && line.Name.Equals("Damage"))
                {
                    line.Text = (int)((gStats.GetDmg() * 0.6) * player.GetDamage(DamageClass.Melee).Multiplicative) + " Light Melee Damage\n" +
                        (int)(gStats.GetDmg() * 2.5 * player.GetDamage(DamageClass.Melee).Multiplicative) + " Heavy Melee Damage";
                }

                if (line.Mod.Equals("Terraria") && line.Name.Equals("Speed"))
                {
                    line.Text = ((gStats.GetCooldown() * player.GetAttackSpeed(DamageClass.Melee)) / 60).ToString("F") + " Seconds to channel";
                }

                if (line.Mod.Equals("Terraria") && line.Name.Equals("Tooltip0"))
                {
                    line.Text = "Hold attack to do greater damage";
                }
            }
        }

        public override void HoldItem(Player player)
        {
            player.statDefense += gStats.defense;
            player.aggro += gStats.aggro;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y = player.position.Y - 62;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            GreatswordPlayer modPlayer = player.GetModPlayer<GreatswordPlayer>();

            if (player.altFunctionUse == 2)
            {
                if (modPlayer.slayerPower >= modPlayer.slayerMax)
                {
                    Item.useStyle = ItemUseStyleID.HoldUp;
                    Item.useTime = 20;
                    Item.useAnimation = 20;
                    modPlayer.slayerPower = 0;
                    player.AddBuff(gStats.buff, 120 + (600 * 1 / modPlayer.slayerMax));
                    Item.shoot = ModContent.ProjectileType<WarriorWraithProj>();
                    SoundEngine.PlaySound(SoundID.Item26, player.position);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Item.DamageType = DamageClass.Melee;
                Item.useTime = 50;
                Item.useAnimation = 50;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.noMelee = true;
                Item.channel = true;
                Item.crit = -4;
                Item.damage = 10;
                Item.knockBack = gStats.GetKnk();
                Item.shoot = greatsword;
            }

            return base.CanUseItem(player) && player.ownedProjectileCounts[Item.shoot] +
                player.ownedProjectileCounts[greatsword] +
                player.ownedProjectileCounts[slash] < 1;
        }
    }
}