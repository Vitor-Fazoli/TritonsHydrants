using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class AngelMouth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angel Mouth");
            Tooltip.SetDefault("All damage taken, damages your mana before dealing damage to your health\n" +
                "your mana dont regenerate\n" +
                "(Mana flower don't work together)");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<Artifact>();
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<Angel>().angel = true;
            player.manaRegen = 0;
            player.manaRegenBonus = 0;
            player.manaRegenCount = 0;
            player.manaRegenBuff = false;
            player.manaRegenDelay = 300;
            player.manaFlower = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Feather, 30)
                .AddIngredient(ItemID.Cloud, 50)
                .AddIngredient(ItemID.ManaPotion, 5)
                .AddIngredient(ItemID.Stinger, 10)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }

    internal class Angel : ModPlayer
    {
        public bool angel = false;
        public override void PreUpdate()
        {
            
            if(angel && Player.statMana >= 0)
            {
                Player.statLife = Player.statLifeMax2;
            }
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (angel)
            {
                Player.statMana -= (int)damage;
            }
        }
    }
}
