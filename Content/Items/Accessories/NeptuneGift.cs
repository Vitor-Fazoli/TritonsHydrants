using MagicTridents.Content.Dusts;
using MagicTridents.Utils;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Timers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.PotionOfReturnGateHelper;

namespace MagicTridents.Content.Items.Accessories
{
    public class NeptuneGift : ModItem
    {
        public override void SetDefaults()
        {
            // Size settings
            Item.width = 28;
            Item.height = 30;

            // Item Properties
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<NeptuneGiftPlayer>().hasNeptuneGift = true;

        }
    }
    internal class NeptuneGiftPlayer : ModPlayer
    {
        public bool hasNeptuneGift = false;
        private float timer;
        private int waterType = Utils.Water.getRandomWater();
        
        public override void ResetEffects()
        {
            hasNeptuneGift = false;
        }

        public override void OnEnterWorld()
        {
            if(hasNeptuneGift)
            {
                timer = 0;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (hasNeptuneGift)
            {
                if (timer >= 600)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                        Dust d = Dust.NewDustPerfect(Player.Center, ModContent.DustType<WaterBubble>(), speed * 5);
                        d.noGravity = true;

                        Lighting.AddLight(Player.position, Water.GetWaterColor().ToVector3());
                    }

                    waterType = Utils.Water.getRandomWater();
                    timer = 0;
                }

                Main.waterStyle = waterType;
                timer++;
            }
        }
    }
}
