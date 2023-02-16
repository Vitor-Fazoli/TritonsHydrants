using DevilsWarehouse.Common.Systems;
using DevilsWarehouse.Content.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    internal class SteroidSyringe : Artifact
    {
        protected override void ModifyTooltipsAgain(List<TooltipLine> tooltips)
        {
            var line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
            switch (god)
            {
                case 0:
                    line.Text = "each critical damage with ranged weapons you drop two Supplies for you and allies spend " +
                        "all of your mana\n each supply heal and increase your defense";
                    break;
                case 1:
                    line.Text = "each critical damage with ranged weapons you receive a buff and heal your life," +
                        " spend all of your mana";
                    break;
                case 2:
                    line.Text = "if you have full health, spend 10% for received a random buff\n" +
                        "this effect happen when you crit with ranged weapons";
                    break;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("only for strong people");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
            godAscended = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            switch (god)
            {
                case 0:
                    player.GetModPlayer<SteroidSyringePlayerZeus>().zeus = true;
                    break;
                case 1:
                    player.GetModPlayer<SteroidSyringePlayerPoseidon>().poseidon = true;
                    break;
                case 2:
                    player.GetModPlayer<SteroidSyringePlayerHades>().hades = true;
                    break;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IllegalGunParts)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    internal class SteroidSyringePlayerZeus : ModPlayer
    {
        public bool zeus;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Ranged && zeus && crit)
            {
                if (Player.statMana >= Player.statManaMax2)
                {
                    Player.statMana -= Player.statManaMax2;

                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X + 100, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SupplyCrate>(), 1);

                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X - 100, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SupplyCrate>(), 1);
                }
            }
        }
        public override void ResetEffects()
        {
            zeus = false;
        }
    }
    internal class SteroidSyringePlayerHades : ModPlayer
    {
        public bool hades;
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Ranged && hades && crit)
            {
                if (Player.statLife == Player.statLifeMax2)
                {
                    Player.statLife -= Player.statLifeMax2 / 10;

                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            Player.AddBuff(ModContent.BuffType<Foward>(), Helper.Ticks(3));
                            break;
                        case 1:
                            Player.AddBuff(ModContent.BuffType<Standing>(), Helper.Ticks(3));
                            break;
                        case 2:
                            Player.AddBuff(ModContent.BuffType<Retreat>(), Helper.Ticks(3));
                            break;
                    }
                }
            }
        }
        public override void ResetEffects()
        {
            hades = false;
        }
    }
    internal class SteroidSyringePlayerPoseidon : ModPlayer
    {
        public bool poseidon;
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Ranged && poseidon && crit)
            {
                if (Player.statMana >= Player.statManaMax2)
                {
                    Player.statMana -= Player.statManaMax2;
                    Player.Heal(50);
                    Player.AddBuff(ModContent.BuffType<Standing>(), 180);
                }
            }
        }
        public override void ResetEffects()
        {
            poseidon = false;
        }
    }
}
