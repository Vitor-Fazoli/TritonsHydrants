using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Accessories
{
    public class SacrificialKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
            Tooltip.SetDefault("Increase 10% in your Critical Chance but after 5 hits you receive damage");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
        }
        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 11;

            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 60);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 0.10f;
            player.GetModPlayer<SacrificialKnifePlayer>().sacrificialKnife = true;
        }
    }
    internal class SacrificialKnifePlayer : ModPlayer
    {
        public bool sacrificialKnife = false;
        private int counter;
        private const int counterMax = 5;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            counter++;
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            counter++;
        }
        public override void UpdateLifeRegen()
        {
            if(counter >= counterMax) 
            {
                Player.statLife -= 20;
                counter = 0;
            }
        }
        public override void OnEnterWorld(Player player)
        {
            counter = 0;
        }
        public override void ResetEffects()
        {
            sacrificialKnife = false;
            counter = 0;
        }
    }
}
