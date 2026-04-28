using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace TritonsHydrants.Content.Items.Accessories
{
    public class SharkMeat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SharkMeatPlayer>().hasSharkMeat = true;
        }
    }

    public class SharkMeatPlayer : ModPlayer
    {
        public bool hasSharkMeat;
        public bool isFrenzied;

        public float frenzyScore;
        public const float MaxFrenzy = 100f;

        private float frenzyTimer;

        public override void ResetEffects()
        {
            hasSharkMeat = false;
        }

        public override void PostUpdate()
        {
            if (!hasSharkMeat)
            {
                frenzyScore = 0;
                isFrenzied = false;
                frenzyTimer = 0;
                return;
            }

            frenzyTimer++;

            if (frenzyTimer >= 30f)
            {
                frenzyTimer = 0f;
                frenzyScore += 5f;
            }

            if (frenzyScore >= MaxFrenzy)
            {
                frenzyScore = MaxFrenzy;
                isFrenzied = true;
            }

            if (isFrenzied)
            {
                frenzyScore -= 1f;

                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustDirect(
                        Player.position,
                        Player.width,
                        Player.height,
                        DustID.Blood
                    );

                    dust.velocity *= 1.5f;
                    dust.scale = 1.3f;
                    dust.noGravity = true;
                }

                if (frenzyScore <= 0f)
                {
                    frenzyScore = 0f;
                    isFrenzied = false;
                }
            }
        }
    }

    public class FrenzyUISystem : ModSystem
    {
        private UserInterface frenzyUI;
        private FrenzyUIState uiState;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                frenzyUI = new UserInterface();
                uiState = new FrenzyUIState();
                frenzyUI.SetState(uiState);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (frenzyUI?.CurrentState != null && Main.LocalPlayer.GetModPlayer<SharkMeatPlayer>().hasSharkMeat)
                frenzyUI.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "TritonsHydrants: Frenzy Bar",
                    delegate
                    {
                        if (Main.LocalPlayer.GetModPlayer<SharkMeatPlayer>().hasSharkMeat)
                            frenzyUI.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }

    public class FrenzyUIState : UIState
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<SharkMeatPlayer>();
            if (!modPlayer.hasSharkMeat) return;

            Texture2D border = ModContent.Request<Texture2D>("TritonsHydrants/Common/Assets/UI/FrenzyBorder").Value;

            Texture2D center = modPlayer.isFrenzied
                ? ModContent.Request<Texture2D>("TritonsHydrants/Common/Assets/UI/FrenzyCenterActive").Value
                : ModContent.Request<Texture2D>("TritonsHydrants/Common/Assets/UI/FrenzyCenter").Value;

            // 📍 Posição (ajustada no passo 3)
            Vector2 screenPos = GetLifeAnchor();

            float scale = (float)modPlayer.frenzyScore / SharkMeatPlayer.MaxFrenzy;

            Rectangle hitbox = new(
                (int)(screenPos.X - border.Width / 2),
                (int)(screenPos.Y - border.Height / 2),
                border.Width,
                border.Height
            );

            spriteBatch.Draw(border, screenPos, null, Color.White, 0f, border.Size() / 2f, 1f, SpriteEffects.None, 0f);

            if (scale > 0f)
            {
                spriteBatch.Draw(center, screenPos, null, Color.White, 0f, center.Size() / 2f, scale, SpriteEffects.None, 0f);
            }

            // 🖱️ Hover
            if (hitbox.Contains(Main.MouseScreen.ToPoint()))
            {
                Main.hoverItemName = $"{(int)modPlayer.frenzyScore} / {SharkMeatPlayer.MaxFrenzy}";
            }
        }

        private static Vector2 GetLifeAnchor()
        {
            // Base do canto superior direito (onde ficam os corações)
            Vector2 pos = new(Main.screenWidth, 0f);

            // Ajuste fino baseado no tamanho da sua textura (30x32)
            pos += new Vector2(-260f, 40f);

            // aplica escala da UI
            pos *= Main.UIScale;

            return pos;
        }
    }
}