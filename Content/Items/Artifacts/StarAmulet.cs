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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StarAmuletP>().starAmulet = true;
            player.manaRegen -= 20;
            player.manaRegenCount = 1;
            player.manaRegenBonus = 0;
            player.manaRegenDelay = 300;
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
        public int cooldown = 0;
        public override void UpdateLifeRegen()
        {
            cooldown--;
        }
        private void ModificationDmgToMana(int damage)
        {
            if (Player.statLife >= Player.statLifeMax2)
            {
                //está com a vida cheia
                if (Player.statMana >= damage && cooldown <= 0)
                {
                    //não recebe dano na vida, para receber na mana
                    Player.immune = true;
                    Player.statMana -= damage;
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 10, 10), Color.BlueViolet, damage);
                    cooldown = 60;
                }else if(Player.statMana >= damage && cooldown >= 0)
                {
                    Player.immune = true;
                }
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (starAmulet)
            {
                ModificationDmgToMana(damage);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (starAmulet)
            {
                ModificationDmgToMana(damage);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (starAmulet)
            {
                if (Player.statMana >= damage)
                {
                    
                }
                return true;
            }
            return false;
        }
        public override void ResetEffects()
        {
            starAmulet = false;
        }
    }
}
