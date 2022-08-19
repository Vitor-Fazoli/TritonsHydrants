using VoidArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class IceShield : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Shield");
            Tooltip.SetDefault("all damage decreased but you spawn with full health\n" +
                "your defense is doubled, " +
                "Significantly higher chance that enemies will target the wearer\n");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<Common.Abstract.ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(damageClass: DamageClass.Generic) -= 0.40f;
            player.aggro += 10;
            player.statDefense = player.statDefense * 2;
            player.GetModPlayer<IceShieldPlayer>().iceShield = true;
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
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                Player.AddBuff(BuffID.Ironskin, 180);
            }
        }
        public override void ResetEffects()
        {
            iceShield = false;
        }
    }
}
