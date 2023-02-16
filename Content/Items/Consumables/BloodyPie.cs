using DevilsWarehouse.Common.Systems.VampireSystem;
using DevilsWarehouse.Content.Buffs.Vampire;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Consumables
{
    public class BloodyPie : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));

            Tooltip.SetDefault("it's dangerous\n" +
                "Transform you in a vampire");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(255, 100, 100),
                new Color(152, 93, 95),
                new Color(174, 192, 192)
            };

            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, ModContent.BuffType<Vampirism>(), 1);
            Item.value = Item.buyPrice(0, 3);
            Item.rare = ItemRarityID.Red;
        }
        public override void OnConsumeItem(Player player)
        {
            player.GetModPlayer<Vampire>().eyeColor = player.eyeColor;
            player.GetModPlayer<Vampire>().vampire = true;
        }
    }
}