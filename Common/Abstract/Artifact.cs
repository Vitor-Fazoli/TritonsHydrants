using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using VoidArsenal.Content.Items;

namespace VoidArsenal.Common.Abstract
{
    public abstract class Artifact : ModItem
    {
        protected int god;
        protected int lvl;
        protected int lvlMax;
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
            god = Main.rand.Next(3);
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
            TooltipLine Find(string name) => tooltips.Find(l => l.Name == name);
            TooltipLine itemName = Find("ItemName");

            var godLine = new TooltipLine(Mod, "Face", "");
            switch (god)
            {
                // Zeus
                case 0:
                    itemName.Text = "Zeus " + itemName.Text;
                    break;
                // Poseidon
                case 1:
                    itemName.Text = "Poseidon " + itemName.Text;
                    break;
                // Hades
                case 2:
                    itemName.Text = "Hades " + itemName.Text;
                    break;
            }
            #endregion

            #region Level
            var levelLine = new TooltipLine(Mod, "Face", $"Level: {lvl}\n {(int)xp} / {xpmax}");
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
            switch (lvl)
            {
                case 25:
                    Item.color = Color.Violet;
                    break;
                case 100:
                    Item.color = Main.DiscoColor;
                    break;
            }
        }
        public override void UpdateEquip(Player player)
        {
            switch (lvl)
            {
                case 25:
                    Item.color = Color.Violet;
                    break;
                case 100:
                    Item.color = Main.DiscoColor;
                    break;
            }

            xp += 0.1; //002
            if (xp >= xpmax)
            {
                xp = 0;
                if(lvl <= lvlMax)
                {
                    lvl++;
                }
            }

            if (!Main.hardMode)
            {
                lvlMax = 25;
            }
            else if(Main.hardMode)
            {
                lvlMax = 100;
            }

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
    }
    internal class ArtifactRarity : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
