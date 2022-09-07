using VoidArsenal.Common;
using VoidArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace VoidArsenal.Content.UI
{
    public class ArtifactSlot : ModAccessorySlot
    {

        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = "Artifact";

            bool flag = Main.mouseLeft && Main.mouseLeftRelease;

            if (flag)
            {
                Main.blockMouse = true;
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

        public override string FunctionalTexture => (GetType().Namespace + "." + Name).Replace('.', '/');
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => checkItem.rare == ModContent.RarityType<ArtifactRarity>();
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.rare == ModContent.RarityType<ArtifactRarity>();
        public override bool IsEnabled() => true;
    }
}
