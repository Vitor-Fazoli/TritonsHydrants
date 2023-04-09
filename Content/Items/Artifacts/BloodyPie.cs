using DevilsWarehouse.Common.Systems;
using DevilsWarehouse.Content.Buffs.Vampire;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DevilsWarehouse.Content.Items.Consumables
{
    public class BloodyPie : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));

            Tooltip.SetDefault("it's dangerous\n" +
                "Transform you in a vampire");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, ModContent.BuffType<Vampirism>(), 1);
            Item.value = Item.buyPrice(0, 3);
            Item.rare = ItemRarityID.Red;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BloodyPieArtifact>().eyeColor = player.eyeColor;
            player.GetModPlayer<BloodyPieArtifact>().vampire = true;
        }
    }

    public class BloodyPieArtifact : ModPlayer
    {
        public Color eyeColor;
        public bool vampire = false;
        public int blood = 0;
        public int bloodMax = 100;
        public int regen = 1;
        public int regenRate = 100;

        private int timer = 0;

        #region getting blood
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (vampire)
            {
                if (Main.rand.NextBool(3) && target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                       0, -2), ModContent.ItemType<LifeEssence>(), 1);
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (vampire)
            {
                if (Main.rand.NextBool(3) && target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                       0, -2), ModContent.ItemType<LifeEssence>(), 1);
                }
            }
        }
        #endregion

        public override void PostUpdate()
        {
            if (vampire)
            {
                if (blood >= bloodMax)
                {
                    blood = bloodMax;
                }

                Player.eyeColor = new Color(255, 0, 0);

                Player.AddBuff(ModContent.BuffType<Vampirism>(), 2);
                bool ZoneSunHeight = (Player.ZoneOverworldHeight || Player.ZoneSkyHeight);

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

                    timer++;

                    if (timer >= regenRate)
                    {
                        blood += regen;
                        timer = 0;
                    }
                }
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
            BloodyPieArtifact clone = clientClone as BloodyPieArtifact;
            clone.vampire = vampire;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            BloodyPieArtifact clone = clientPlayer as BloodyPieArtifact;

            if (vampire != clone.vampire)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["vampire"] = vampire;
            tag["eyeColor"] = eyeColor;
        }

        public override void LoadData(TagCompound tag)
        {
            vampire = tag.GetBool("vampire");
            eyeColor = tag.Get<Color>("eyeColor");
        }
        #endregion
    }
}