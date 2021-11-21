using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Utilities;

namespace GearonArsenalMod.Content.NPCs.Enemies
{
    public class BloodWorm : ModNPC
    {
        public override void SetStaticDefaults()
        {
			// The name the enemy displays
			DisplayName.SetDefault("Blood Worm");

			Main.npcFrameCount[NPC.type] = 5;
        }
		
		public override void SetDefaults()
        {
            // Enemy hitbox
            NPC.width = 46;
			NPC.height = 42;
			// The AI Style
			NPC.aiStyle = 3;
			// Enemy damage and defense
			NPC.damage = 1;
			NPC.defense = 1;
			// Enemy max health
			NPC.lifeMax = 200;
			// Enemy sound upon hit or death
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			// How much the knockback is resisted
			NPC.knockBackResist = 0.75f;
		}
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = 0 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.frameCounter < 40)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else if (NPC.frameCounter < 50)
            {
                NPC.frame.Y = 4 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }

            NPC.spriteDirection = -NPC.direction;
        }

        // public override void HitEffect
    }
}
