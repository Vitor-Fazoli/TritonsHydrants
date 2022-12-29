using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.NPCs.Bosses
{
    public class Vanael : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 120;
            NPC.lifeMax = 10000;
            NPC.boss = true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            Vector2 moveTo = player.Center + new Vector2(100f, -50f);

            float speed = 5f;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            move *= speed / magnitude; 
            NPC.velocity = move;
        }
    }
}
