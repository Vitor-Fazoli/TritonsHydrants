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
    public class Vampire : ModPlayer
    {
        public bool vampire = false;
        public int blood = 0;
        public int bloodMax = 100;

        public override void PostUpdate()
        {
            if (vampire)
            {
                Player.eyeColor = new Color(255, 0, 0);

                Player.AddBuff(ModContent.BuffType<Vampirism>(), 2);
                 bool ZoneSunHeight = (Player.ZoneOverworldHeight || Player.ZoneSkyHeight);

                //Day and Night buffs
                if (Main.dayTime)
                {
                    if (Player.behindBackWall || !ZoneSunHeight)
                    {
                        Player.AddBuff(ModContent.BuffType<Night>(), 2);
                    }
                    else
                    {
                        Player.AddBuff(ModContent.BuffType<Day>(), 2);
                    }
                }
                else
                {
                    Player.AddBuff(ModContent.BuffType<Night>(), 2);
                }
                //Bat Form
            }
        }
        #region data saving
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(vampire);
            packet.Send(toWho, fromWho);
        }
        public override void clientClone(ModPlayer clientClone)
        {
            Vampire clone = clientClone as Vampire;
            clone.vampire  = vampire;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            Vampire clone = clientPlayer as Vampire;
                
            if (vampire != clone.vampire)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["vampire"] = vampire;
        }

        public override void LoadData(TagCompound tag)
        {
            vampire = tag.GetBool("vampire");
        }
        #endregion
    }
}