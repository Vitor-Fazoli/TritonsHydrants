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
        protected bool sun;
        protected int godID;
        public Color moonColor = new(167, 65, 211);
        public Color SunColor = new(255, 192, 56);
        public override void OnCreate(ItemCreationContext context)
        {
            sun = Main.rand.NextBool(2);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            switch (godID)
            {
                // Sun God
                case 0:
                    break;
                // Moon God
                case 1:
                    break;
                // Hammer God
                case 2:
                    break;
            }
            if (sun)
            {
                var line = new TooltipLine(Mod, "Face", "Sun God")
                {
                    OverrideColor = SunColor
                };
                tooltips.Add(line);
            }
            else
            {
                var line = new TooltipLine(Mod, "Face", "Moon God")
                {
                    OverrideColor = moonColor
                };
                tooltips.Add(line);
            }

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
            sun = tag.GetBool("sun");
        }
        public override void SaveData(TagCompound tag)
        {
            tag["sun"] = sun;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(sun);
        }
    }
    internal class ArtifactRarity : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
