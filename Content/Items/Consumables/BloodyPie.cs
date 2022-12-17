using DevilsWarehouse.Content.Buffs;
using DevilsWarehouse.Content.Items.Artifacts;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DevilsWarehouse.Content.Items.Consumables
{
    public class BloodyPie : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Delicious Bloody Pie");

            Tooltip.SetDefault("");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(249, 230, 136),
                new Color(152, 93, 95),
                new Color(174, 192, 192)
            };

            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.WellFed3, 57600);
            Item.value = Item.buyPrice(0, 3);
            Item.rare = ItemRarityID.Blue;
        }
        public override void OnConsumeItem(Player player)
        {
            player.GetModPlayer<Vampire>().vampire = true;
        }
    }

    internal class Vampire : ModPlayer
    {
        public bool vampire = false;

        public override void PostUpdate()
        {
            if (vampire)
            {
                 bool ZoneSunHeight = (Player.ZoneOverworldHeight || Player.ZoneSkyHeight);

                //Day and Night buffs
                if (Main.dayTime)
                {
                    if (Player.behindBackWall || !ZoneSunHeight)
                    {
                        //Player.AddBuff(Modcontent.BuffType<>(), 2); gift of shadows
                    }
                    else
                    {
                        Player.AddBuff(BuffID.OnFire, 2);
                    }
                }
                else
                {
                    //Player.AddBuff(Modcontent.BuffType<>(), 2); gift of powerful shadows
                }
                //Bat Form
            }
        }
    }
}