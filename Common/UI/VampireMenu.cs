using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace DevilsWarehouse.Common.UI
{
    internal class VampireMenu
    {
        public class ExampleCoinsUISystem : ModSystem
        {
            private UserInterface exampleCoinUserInterface;
            internal ExampleCoinsUIState exampleCoinsUI;

            // These two methods will set the state of our custom UI, causing it to show or hide
            public void ShowMyUI()
            {
                exampleCoinUserInterface?.SetState(exampleCoinsUI);
            }

            public void HideMyUI()
            {
                exampleCoinUserInterface?.SetState(null);
            }

            public override void Load()
            {
                // Create custom interface which can swap between different UIStates
                exampleCoinUserInterface = new UserInterface();
                // Creating custom UIState
                exampleCoinsUI = new ExampleCoinsUIState();

                // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
                exampleCoinsUI.Activate();
            }

            public override void UpdateUI(GameTime gameTime)
            {
                // Here we call .Update on our custom UI and propagate it to its state and underlying elements
                if (exampleCoinUserInterface?.CurrentState != null)
                    exampleCoinUserInterface?.Update(gameTime);
            }

            // Adding a custom layer to the vanilla layer list that will call .Draw on your interface if it has a state
            // Setting the InterfaceScaleType to UI for appropriate UI scaling
            public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
            {
                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                if (mouseTextIndex != -1)
                {
                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                        "ExampleMod: Coins Per Minute",
                        delegate {
                            if (exampleCoinUserInterface?.CurrentState != null)
                                exampleCoinUserInterface.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }

        internal class ExampleCoinsUIState : UIState
        {
            public ExampleDragableUIPanel CoinCounterPanel;
            public UIMoneyDisplay MoneyDisplay;

            // In OnInitialize, we place various UIElements onto our UIState (this class).
            // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
            // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
            public override void OnInitialize()
            {
                // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
                CoinCounterPanel = new ExampleDragableUIPanel();
                CoinCounterPanel.SetPadding(0);
                // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(coinCounterPanel);`. 
                // This means that this class, ExampleCoinsUI, will be our Parent. Since ExampleCoinsUI is a UIState, the Left and Top are relative to the top left of the screen.
                // SetRectangle method help us to set the position and size of UIElement
                SetRectangle(CoinCounterPanel, left: 400f, top: 100f, width: 170f, height: 70f);
                CoinCounterPanel.BackgroundColor = new Color(73, 94, 171);

                // Next, we create another UIElement that we will place. Since we will be calling `coinCounterPanel.Append(playButton);`, Left and Top are relative to the top left of the coinCounterPanel UIElement. 
                // By properly nesting UIElements, we can position things relatively to each other easily.
                Asset<Texture2D> buttonPlayTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay");
                ExitButton playButton = new ExitButton(buttonPlayTexture, "Reset Coins Per Minute Counter");
                SetRectangle(playButton, left: 110f, top: 10f, width: 22f, height: 22f);
                // UIHoverImageButton doesn't do anything when Clicked. Here we assign a method that we'd like to be called when the button is clicked.
                playButton.OnClick += new MouseEvent(PlayButtonClicked);
                CoinCounterPanel.Append(playButton);

                Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
                ExitButton closeButton = new ExitButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
                SetRectangle(closeButton, left: 140f, top: 10f, width: 22f, height: 22f);
                closeButton.OnClick += new MouseEvent(CloseButtonClicked);
                CoinCounterPanel.Append(closeButton);

                // UIMoneyDisplay is a fairly complicated custom UIElement. UIMoneyDisplay handles drawing some text and coin textures.
                // Organization is key to managing UI design. Making a contained UIElement like UIMoneyDisplay will make many things easier.
                MoneyDisplay = new UIMoneyDisplay();
                SetRectangle(MoneyDisplay, 15f, 20f, 100f, 40f);
                CoinCounterPanel.Append(MoneyDisplay);

                Append(CoinCounterPanel);
                // As a recap, ExampleCoinsUI is a UIState, meaning it covers the whole screen. We attach coinCounterPanel to ExampleCoinsUI some distance from the top left corner.
                // We then place playButton, closeButton, and moneyDiplay onto coinCounterPanel so we can easily place these UIElements relative to coinCounterPanel.
                // Since coinCounterPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when coinCounterPanel moves.
            }

            private void SetRectangle(UIElement uiElement, float left, float top, float width, float height)
            {
                uiElement.Left.Set(left, 0f);
                uiElement.Top.Set(top, 0f);
                uiElement.Width.Set(width, 0f);
                uiElement.Height.Set(height, 0f);
            }

            private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                MoneyDisplay.ResetCoins();
            }

            private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                ModContent.GetInstance<ExampleCoinsUISystem>().HideMyUI();
            }

            public void UpdateValue(int pickedUp)
            {
                MoneyDisplay.AddCoinsPerMinute(pickedUp);
            }
        }

        public class UIMoneyDisplay : UIElement
        {
            // How many coins have been collected in copper
            public long collectedCoins;
            // Time from start(or reset) to calculate how many coins collected per minute
            private DateTime? startTime;
            // Saving coin textures to an array to make them easier to access
            private readonly Texture2D[] coinsTextures = new Texture2D[4];

            public UIMoneyDisplay()
            {
                startTime = null;

                for (int j = 0; j < 4; j++)
                {
                    // Textures may not be loaded without it
                    Main.instance.LoadItem(74 - j);
                    coinsTextures[j] = TextureAssets.Item[74 - j].Value;
                }
            }
            public void AddCoinsPerMinute(int coins)
            {
                collectedCoins += coins;

                // We begin to remember the time only after at least one coin has been collected
                if (startTime == null)
                    startTime = DateTime.Now;
            }

            public int GetCoinsPerMinute()
            {
                if (collectedCoins == 0)
                    return 0;

                // If the time has passed less than minutes, the current number of coins will be displayed
                return (int)(collectedCoins / Math.Max(1, (DateTime.Now - startTime.Value).TotalMinutes));
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                CalculatedStyle innerDimensions = GetInnerDimensions();
                // Getting top left position of this UIElement
                float shopx = innerDimensions.X;
                float shopy = innerDimensions.Y;

                // Drawing first line of coins (current collected coins)
                // CoinsSplit converts the number of copper coins into an array of all types of coins
                DrawCoins(spriteBatch, shopx, shopy, Utils.CoinsSplit(collectedCoins));

                // Drawing second line of coins (coins per minute) and text "CPM"
                DrawCoins(spriteBatch, shopx, shopy, Utils.CoinsSplit(GetCoinsPerMinute()), 0, 25);
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, "CPM", shopx + (float)(24 * 4), shopy + 25f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
            }

            private void DrawCoins(SpriteBatch spriteBatch, float shopx, float shopy, int[] coinsArray, int xOffset = 0, int yOffset = 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    spriteBatch.Draw(coinsTextures[j], new Vector2(shopx + 11f + 24 * j + xOffset, shopy + yOffset), null, Color.White, 0f, coinsTextures[j].Size() / 2f, 1f, SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, coinsArray[3 - j].ToString(), shopx + 24 * j + xOffset, shopy + yOffset, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
                }
            }

            public void ResetCoins()
            {
                collectedCoins = 0;
                startTime = DateTime.Now;
            }
        }

        public class MoneyCounterGlobalItem : GlobalItem
        {
            public override bool OnPickup(Item item, Player player)
            {
                // If we have picked up coins of any type, then we will update the values in exampleCoinsUI
                if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin)
                    ModContent.GetInstance<ExampleCoinsUISystem>().exampleCoinsUI.UpdateValue(item.stack * (item.value / 5));

                return base.OnPickup(item, player);
            }
        }

        internal class ExitButton : UIImageButton
        {
            // Tooltip text that will be shown on hover
            internal string hoverText;

            public ExitButton(Asset<Texture2D> texture, string hoverText) : base(texture)
            {
                this.hoverText = hoverText;
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);

                if (IsMouseHovering)
                    Main.hoverItemName = hoverText;
            }
        }
        public class ExampleDragableUIPanel : UIPanel
        {
            // Stores the offset from the top left of the UIPanel while dragging
            private Vector2 offset;
            // A flag that checks if the panel is currently being dragged
            private bool dragging;

            public override void MouseDown(UIMouseEvent evt)
            {
                // When you override UIElement methods, don't forget call the base method
                // This helps to keep the basic behavior of the UIElement
                base.MouseDown(evt);
                // When the mouse button is down, then we start dragging
                DragStart(evt);
            }

            public override void MouseUp(UIMouseEvent evt)
            {
                base.MouseUp(evt);
                // When the mouse button is up, then we stop dragging
                DragEnd(evt);
            }

            private void DragStart(UIMouseEvent evt)
            {
                // The offset variable helps to remember the position of the panel relative to the mouse position
                // So no matter where you start dragging the panel, it will move smoothly
                offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
                dragging = true;
            }

            private void DragEnd(UIMouseEvent evt)
            {
                Vector2 endMousePosition = evt.MousePosition;
                dragging = false;

                Left.Set(endMousePosition.X - offset.X, 0f);
                Top.Set(endMousePosition.Y - offset.Y, 0f);

                Recalculate();
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                // Checking ContainsPoint and then setting mouseInterface to true is very common
                // This causes clicks on this UIElement to not cause the player to use current items
                if (ContainsPoint(Main.MouseScreen))
                {
                    Main.LocalPlayer.mouseInterface = true;
                }

                if (dragging)
                {
                    Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same
                    Top.Set(Main.mouseY - offset.Y, 0f);
                    Recalculate();
                }

                // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle
                // (In our example, the parent would be ExampleCoinsUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
                // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution
                var parentSpace = Parent.GetDimensions().ToRectangle();
                if (!GetDimensions().ToRectangle().Intersects(parentSpace))
                {
                    Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                    Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                    // Recalculate forces the UI system to do the positioning math again.
                    Recalculate();
                }
            }
        }
    }
}
