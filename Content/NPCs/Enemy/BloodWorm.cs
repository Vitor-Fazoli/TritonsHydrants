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

namespace GearonArsenalMod.Content.NPCs.Enemy
{
    public class BloodWorm : ModNPC
    {
        public override void SetStaticDefaults()
        {
			// The name the enemy displays
			DisplayName.SetDefault("Blood Worm");
        }
		
		public override void SetDefaults()
        {
            // Enemy hitbox
            NPC.width = 12;
			NPC.height = 8;
			// The AI Style
			NPC.aiStyle = 3;
			// Enemy damage and defense
			NPC.damage = 1;
			NPC.defense = 1;
			// Enemy max health
			NPC.lifeMax = 1;
			// Enemy sound upon hit or death
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			// How much the knockback is resisted
			NPC.knockBackResist = 0.75f;
		}

		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return SpawnCondition.OverworldBloodMoon
        }*/

		public override void HitEffect
    }
}