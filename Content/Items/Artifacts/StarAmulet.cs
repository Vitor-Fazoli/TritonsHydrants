using VoidArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    public class StarAmulet : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Amulet");
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
            player.GetModPlayer<StarAmuletP>().starAmulet = true;
            player.manaRegen = 0;
            player.manaRegenCount = 0;
            player.manaFlower = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 25)
                .AddIngredient(ItemID.CobaltShield)
                .AddIngredient(ItemID.Star, 10)
                .AddTile(TileID.Hellforge)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 25)
                .AddIngredient(ItemID.CobaltShield)
                .AddIngredient(ItemID.Star, 10)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
    internal class StarAmuletP : ModPlayer
    {
        public bool starAmulet = false;

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (starAmulet)
            {
                if (Player.statMana >= damage)
                {
                    int dmgAux = damage;
                    damage = 0;
                    Player.statMana -= dmgAux;
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 10, 10), Color.BlueViolet, dmgAux);
                }
                else if (Player.statMana != 0)
                {
                    int dmgResult = Player.statMana - damage;
                    Player.statMana = 0;
                    damage = dmgResult;
                }
                return true;
            }
            return false;
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (starAmulet)
            {
                if (Player.statMana >= 0)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (Main.combatText[i].text == damage.ToString() && Main.combatText[i].color == CombatText.DamagedFriendly)
                        {
                            Main.combatText[i].active = false;
                        }
                    }
                }
            }
        }
    }
}
