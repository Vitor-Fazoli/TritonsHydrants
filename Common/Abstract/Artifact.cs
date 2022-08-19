using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace VoidArsenal.Common.Abstract
{
    public abstract class Artifact : ModItem
    {
        public bool godReady;
        public int god;
        protected int lvl;
        protected int lvlMax = 500;
        public double xp;
        protected const int xpmax = 300;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 3));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.CanGetPrefixes[Item.type] = false;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void OnCreate(ItemCreationContext context)
        {
            if (godReady)
            {
                god = Main.rand.Next(3);
            }
            lvl = 1;
            xp = 0;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            switch (pre)
            {
                case -3:
                    return false;
                case -1:
                    return false;
            }
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            #region God
            if (godReady)
            {
                TooltipLine Find(string name) => tooltips.Find(l => l.Name == name);
                TooltipLine itemName = Find("ItemName");

                var godLine = new TooltipLine(Mod, "Face", "");
                switch (god)
                {
                    // Zeus
                    case 0:
                        itemName.Text = itemName.Text + " of Zeus";
                        break;
                    // Poseidon
                    case 1:
                        itemName.Text = itemName.Text + " of Poseidon";
                        break;
                    // Hades
                    case 2:
                        itemName.Text = itemName.Text + " of Hades";
                        break;
                }
            }
            #endregion

            #region Level
            var levelLine = new TooltipLine(Mod, "Face", $"Level: {lvl}\n | {(int)xp} | {xpmax} |");
            tooltips.Add(levelLine);
            #endregion

            ModifyCreation(tooltips);
        }
        public override bool? CanBurnInLava()
        {
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            LevelCap();
            ArtifactLevelColor();
        }
        public override void UpdateEquip(Player player)
        {
            LevelCap();
            ArtifactLevelColor();

            xp += 0.002; // Default: 002
            if (xp >= xpmax)
            {
                xp = 0;
                if(lvl <= lvlMax)
                {
                    lvl++;
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Item.defense += lvl / 10;
            player.GetDamage(DamageClass.Generic) += (0.0005f * lvl);
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (modded)
            {
                return true;
            }
            return false;
        }

        #region Effects in world
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);
            ArtifactLevelColor();
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new(255, 255, 255, 0), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new(255, 255, 255, 0), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        #endregion

        #region Data Saving
        public override void LoadData(TagCompound tag)
        {
            god = tag.GetInt("godID");
            lvl = tag.GetInt("lvlID");
            xp = tag.GetDouble("xpID");
        }
        public override void SaveData(TagCompound tag)
        {
            tag["lvlID"] = lvl;
            tag["godID"] = god;
            tag["xpID"] = xp;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(god);
            writer.Write(lvl);
            writer.Write(xp);
        }
        #endregion

        protected virtual void ModifyCreation(List<TooltipLine> tooltips) { }

        protected void ArtifactLevelColor()
        {
            switch (lvl)
            {
                case 15:
                    Item.color = new Color(Main.DiscoR, 255, 255);
                    break;
                case 100:
                    Item.color = new(Main.DiscoR, Main.DiscoG, 255, Main.DiscoB / 2);
                    break;
            }
        }
        private void LevelCap()
        {
            if (!Main.hardMode)
            {
                lvlMax = 25;
            }
            else if (Main.hardMode)
            {
                lvlMax = 500;
            }
        }
    }
    internal class ArtifactRarity : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
