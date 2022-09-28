using DevilsWarehouse.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.UI
{
    public class ArtifactSlot : ModAccessorySlot
    {
        private int time;
        private readonly int cooldown = 10;
        public override void Load()
        {
            time = Readability.toSeconds(cooldown);
        }
        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = "Artifact";

            if (IsEmpty && time != 0)
            {
                Main.hoverItemName = (time / 60).ToString() + " s";
            }
        }
        public override bool IsHidden()
        {
            return false;
        }
        #region without more slots
        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;
        #endregion
        public override void PostDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            #region Time System
            if (time <= 0)
            {
                time = 0;
            }

            if (IsEmpty && time != 0)
            {
                --time;
            }

            if (!IsEmpty)
            {
                time = Readability.toSeconds(cooldown);
            }
            #endregion
        }
        public override string FunctionalBackgroundTexture => "DevilsWarehouse/Content/UI/ArtifactBackground";
        public override string FunctionalTexture => (GetType().Namespace + "." + Name).Replace('.', '/');
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return checkItem.rare == ModContent.RarityType<ArtifactRarity>() && IsEmpty;
        }
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.rare == ModContent.RarityType<ArtifactRarity>() && IsEmpty;
        public override bool IsEnabled() => (time == 0  && IsEmpty) || IsEmpty == false;
        public override bool IsVisibleWhenNotEnabled() => true;
    }
}
