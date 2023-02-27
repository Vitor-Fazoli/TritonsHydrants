using DevilsWarehouse.Common.Systems;
using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class IceShield : Artifact
    {
        private int quantity;
        private int distance;
        public override void Load()
        {
            distance = 60;
            quantity = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("all damage decreased but you spawn with full health\n" +
                "your defense is doubled, knockback immunity" +
                "Significantly higher chance that enemies will target the wearer\n");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(damageClass: DamageClass.Generic) -= 0.40f;
            player.aggro += 10;
            player.statDefense = player.statDefense * 2;
            player.GetModPlayer<IceShieldPlayer>().iceShield = true;
            player.noKnockback = true;

            if (distance <= 0 && quantity < 3)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.IceOrb>(), 5 + player.statDefense, 20, player.whoAmI);
                player.AddBuff(ModContent.BuffType<Buffs.IceOrb>(), 2);
                distance = 120;
                quantity++;
            }

            distance--;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PaladinsShield)
                .AddIngredient(ItemID.IceMachine)
                .AddIngredient(ItemID.IronskinPotion, 10)
                .AddIngredient(ItemID.CopperBar, 40)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
    internal class IceShieldPlayer : ModPlayer
    {
        public bool iceShield = false;
        public override void OnRespawn(Player player)
        {
            if (iceShield)
            {
                player.statLife = player.statLifeMax2;
            }
        }
        public override void UpdateEquips()
        {
            if (!iceShield)
            {
                Player.ClearBuff(ModContent.BuffType<Buffs.IceOrb>());
            }
        }
        public override void ResetEffects()
        {
            iceShield = false;
        }
    }
}
