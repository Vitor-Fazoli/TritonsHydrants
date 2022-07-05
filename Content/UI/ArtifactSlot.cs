using GearonArsenal.Common;
using GearonArsenal.Common.Abstract;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace GearonArsenal.Content.UI
{
    public class ArtifactSlot : ModAccessorySlot
    {
        
        public bool enable;

        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = "Artifact";
        }
        public override bool IsHidden()
        {
            return false;
        }
        public override bool DrawVanitySlot => false;

        public override bool DrawDyeSlot => false;

        public override string FunctionalTexture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (checkItem.rare == ModContent.RarityType<ArtifactR>())
                return true;

            return false;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            if (item.rare == ModContent.RarityType<ArtifactR>())
                return true;

            return false;
        }
        public override bool IsEnabled()
        {
            return true;
        }
    }
}
