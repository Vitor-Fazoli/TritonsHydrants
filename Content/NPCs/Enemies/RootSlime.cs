using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace DevilsWarehouse.Content.NPCs.Enemies
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
            NPC.aiStyle = NPCID.DungeonSlime;
            AIType = NPCID.DungeonSlime;
            AnimationType = NPCID.BlueSlime;
            NPC.friendly = false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.SurfaceJungle.Chance * 0.09f;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (crit)
            {
                target.AddBuff(BuffID.Poisoned, 120);
            }
        }
        public override void OnKill()
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Utils.RandomVector2(Main.rand, -1f, 1f);
                Dust d = Dust.NewDustPerfect(NPC.Center,DustID.t_Slime, speed * 5,newColor: Color.DarkGreen);
                d.noGravity = true;
            }
        }
    }
}
