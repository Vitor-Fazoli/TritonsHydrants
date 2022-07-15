using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using VoidArsenal.Content.Items;

namespace VoidArsenal.Common.Abstract
{
    public abstract class Artifact : ModItem
    {
        protected int Gem;

        private Color Sapphire = new(15, 82, 186);
        private Color Ruby = new(224, 17, 95);
        private Color Emerald = new(80, 200, 120);
        public override void OnCreate(ItemCreationContext context)
        {
            Gem = Main.rand.Next(3);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod,"Face","");
            switch (Gem)
            {
                // Ruby
                case 0:
                    line = new TooltipLine(Mod, "Face", "Ruby")
                    {
                        OverrideColor = Ruby
                    };
                    break;
                // Sapphire
                case 1:
                    line = new TooltipLine(Mod, "Face", "Sapphire")
                    {
                        OverrideColor = Sapphire
                    };
                    break;
                // Emerald
                case 2:
                    line = new TooltipLine(Mod, "Face", "Emerald")
                    {
                        OverrideColor = Emerald
                    };
                    break;
            }
            tooltips.Add(line);
            ModifyCreation(tooltips);
        }
        public override bool? CanBurnInLava()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
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
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (modded)
            {
                return true;
            }
            return false;
        }
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
        //Back-end
        public override void LoadData(TagCompound tag)
        {
            Gem = tag.GetInt("godID");
        }
        public override void SaveData(TagCompound tag)
        {
            tag["godID"] = Gem;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(Gem);
        }
        protected virtual void ModifyCreation(List<TooltipLine> tooltips)
        {

        }
    }
    internal class ArtifactRarity : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
