using DevilsWarehouse.Content.Buffs.Vampire;
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
            Tooltip.SetDefault("Increase your Critical Chance but receive damage ");
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
        public int cooldown;
        public int cooldownMax = Helper.Ticks(20);
        public bool sacrificialKnife = false;

        public override void Load()
        {
            cooldown = cooldownMax;
        }
        public override void UpdateBadLifeRegen()
        {
            if (sacrificialKnife && cooldown <= 0)
            {
                HurtEffect();

                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 50;
                cooldown = cooldownMax;
            }
        }
        public override void PostUpdate()
        {
            if (sacrificialKnife)
            {
                cooldown--;
            }
        }
        private void HurtEffect()
        {
            //TODO: Implementar um projétil para aparecer em cima da cabeça do player - uma mão se cortando com uma faca
        }
    }
}
