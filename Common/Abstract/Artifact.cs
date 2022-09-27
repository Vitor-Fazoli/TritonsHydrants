using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace VoidArsenal.Common.Abstract
{
    public abstract class Artifact : ModItem
    {
        public bool godAscended;
        public int god;

        public override void OnCreate(ItemCreationContext context)
        {
            if (godAscended)
            {
                god = Main.rand.Next(3);
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return pre switch
            {
                -3 => false,
                -1 => false,
                _ => (bool?)true,
            };
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            SystemGod(tooltips, Mod);

            ModifyTooltipsAgain(tooltips);
        }

        public override bool? CanBurnInLava()
        {
            return false;
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
        public void PostUpdate(Item item)
        {
            Lighting.AddLight(item.Center, Color.White.ToVector3() * 0.4f);
        }
        public static bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[item.type] != null)
            {
                frame = Main.itemAnimations[item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(item.width / 2 - frameOrigin.X, item.height - frame.Height);
            Vector2 drawPos = item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = item.timeSinceItemSpawned / 240f + time * 0.04f;

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
        }
        public override void SaveData(TagCompound tag)
        {
            tag["godID"] = god;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(god);
        }
        #endregion
        private void SystemGod(List<TooltipLine> tooltips, Mod mod)
        {
            if (godAscended)
            {
                TooltipLine Find(string name) => tooltips.Find(l => l.Name == name);
                TooltipLine itemName = Find("ItemName");

                var godLine = new TooltipLine(mod, "Face", "");
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
        }
        protected virtual void ModifyTooltipsAgain(List<TooltipLine> tooltips) { }
    }
    internal class ArtifactRarity : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
