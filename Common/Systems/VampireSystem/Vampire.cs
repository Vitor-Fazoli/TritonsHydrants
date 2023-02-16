using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using DevilsWarehouse.Content.Items;
using Terraria.DataStructures;
using Terraria.ID;
using DevilsWarehouse.Content.Buffs.Vampire;

namespace DevilsWarehouse.Common.Systems.VampireSystem
{
    public class Vampire : ModPlayer
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

                    timer++;

                    if (timer >= regenRate)
                    {
                        blood += regen;
                        timer = 0;
                    }
                }
                //Silver debuffs
                if (CursedSilver())
                {
                    Player.AddBuff(ModContent.BuffType<Silver>(), 2);
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
            tag["eyeColor"] = eyeColor;
        }

        public override void LoadData(TagCompound tag)
        {
            vampire = tag.GetBool("vampire");
            eyeColor = tag.Get<Color>("eyeColor");
        }
        #endregion
        public bool CursedSilver()
        {
            for (int i = 0; i < Player.inventory.Length; i++)
            {
                switch (Player.inventory[i].type)
                {
                    case ItemID.SilverAxe:
                        return true;
                    case ItemID.SilverBar:
                        return true;
                    case ItemID.SilverBow:
                        return true;
                    case ItemID.SilverOre:
                        return true;
                    case ItemID.SilverGreaves:
                        return true;
                    case ItemID.SilverHelmet:
                        return true;
                    case ItemID.SilverPickaxe:
                        return true;
                    case ItemID.SilverShortsword:
                        return true;
                    case ItemID.SilverWatch:
                        return true;
                    case ItemID.SilverBrick:
                        return true;
                    case ItemID.SilverBrickWall:
                        return true;
                    case ItemID.SilverBullet:
                        return true;
                    case ItemID.SilverChainmail:
                        return true;
                    case ItemID.SilverChandelier:
                        return true;
                    case ItemID.SilverCoin:
                        return true;
                    case ItemID.SilverHammer:
                        return true;
                    case ItemID.GolfTrophySilver:
                        return true;
                }
            }
            return false;
        }
    }
}
