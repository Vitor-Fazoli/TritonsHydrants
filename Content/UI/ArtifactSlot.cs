using VoidArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.UI
{
    public class ArtifactSlot : ModAccessorySlot
    {
        int time = 0;

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
            if(time <= 0)
            {
                time = 0;
            }

            if (IsEmpty && time != 0)
            {
                --time;
            }

            if (!IsEmpty)
            {
                time = 1800;
            }
        }
        
        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back7";
        public override string FunctionalTexture => (GetType().Namespace + "." + Name).Replace('.', '/');
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return checkItem.rare == ModContent.RarityType<ArtifactRarity>() && IsEmpty;
        }
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.rare == ModContent.RarityType<ArtifactRarity>() && IsEmpty;
        public override bool IsEnabled() => time == 0;
        public override bool IsVisibleWhenNotEnabled() => true;
    }
}
