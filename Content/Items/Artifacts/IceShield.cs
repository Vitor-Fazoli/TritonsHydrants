using GearonArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class IceShield : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Shield");
            Tooltip.SetDefault("all damage decreased but you spawn with full health\n" +
                "your defense is doubled" +
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
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(damageClass: DamageClass.Generic) -= 0.40f;
            player.aggro += 10;
            player.statDefense = player.statDefense * 2;
            player.GetModPlayer<IceShieldP>().protector = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PaladinsShield)
                .AddIngredient(ItemID.IceBlade)
                .AddIngredient(ItemID.IceMachine)
                .AddIngredient(ItemID.IronskinPotion, 10)
                .AddIngredient(ItemID.CopperBar, 40)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
    internal class IceShieldP : ModPlayer
    {
        public bool protector = false;
        public override void OnRespawn(Player player)
        {
            if (protector)
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
    }
}
