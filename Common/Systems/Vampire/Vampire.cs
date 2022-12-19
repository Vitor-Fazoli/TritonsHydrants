using DevilsWarehouse.Content.Buffs;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using DevilsWarehouse.Content.Items;
using Terraria.DataStructures;
using Terraria.ID;

namespace DevilsWarehouse.Common.Systems.Vampire
{
    public class Vampire : ModPlayer
    {
        public bool vampire = false;
        public int blood = 0;
        public int bloodMax = 100;


        #region getting blood
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(2) && target.type != NPCID.TargetDummy)
            {
                Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 5 + Main.rand.Next(5)), new Vector2(
                   0, -5), ModContent.ItemType<LifeEssence>(), 1);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life >= target.lifeMax / 2)
            {
                if (Main.rand.NextBool(2) && target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 5 + Main.rand.Next(5)), new Vector2(
                       0, -5), ModContent.ItemType<LifeEssence>(), 1);
                }
            }
        }
        #endregion
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
            clone.vampire = vampire;
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
