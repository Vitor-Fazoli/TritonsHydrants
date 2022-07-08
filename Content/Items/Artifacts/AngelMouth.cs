using VoidArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class AngelMouth : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angel Mouth");
            Tooltip.SetDefault("");
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
            player.GetModPlayer<AngelMouthP>().angelMouth = true;
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
    internal class AngelMouthP : ModPlayer
    {
        public bool angelMouth = false;

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (angelMouth)
            {
                if(Player.statMana >= damage)
                {
                    int dmgAux = damage;
                    damage = 0;
                    Player.statMana -= dmgAux;
                }
            }
            return true;
        }
    }
}
