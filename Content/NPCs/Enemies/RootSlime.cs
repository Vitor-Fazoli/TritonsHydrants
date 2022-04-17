using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.NPCs.Enemies
{
    internal class RootSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 180;
            NPC.height = 180;

            NPC.damage = 14;
            NPC.defense = 6;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = NPCID.BlueSlime;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }
    }
}
