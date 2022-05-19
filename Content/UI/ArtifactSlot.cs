using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GearonArsenal.Content.UI
{
    public class ArtifactSlot : ModAccessorySlot
    {
        public override bool IsHidden()
        {
            return false;
        }

        public override bool DrawVanitySlot => false;

        public override bool DrawDyeSlot => false;

        public override string FunctionalTexture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (checkItem.rare == ModContent.RarityType<Artifact>())
                return true;

            return false;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            if (item.rare == ModContent.RarityType<Artifact>())
                return true;

            return false;
        }
        public override bool IsEnabled()
        {
            return true;
        }
    }
}
