using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace DevilsWarehouse.Content.NPCs
{
    internal class FightsabreReflect : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[NPC.type] = 14;
        }
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 22;
            NPC.reflectsProjectiles = true;
        }
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }
        public override bool PreAI()
        {
            Vector2 offset = new(-80,-60);


            return true;
        }
    }
}
