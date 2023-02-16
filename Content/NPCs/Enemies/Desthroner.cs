using DevilsWarehouse.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.NPCs.Enemies
{
    public class Desthroner : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 65;
            NPC.height = 50;
            NPC.lifeMax = 450;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.defense = 10;
            NPC.knockBackResist = 0.5f;
            NPC.lavaImmune = true;
            NPC.npcSlots = 5f;
            NPC.damage = 40;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.velocity *= 1.75f;
            NPC.aiStyle = NPCAIStyleID.FlyingFish;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneDesert && !Main.dayTime)
            {
                return 0.29f;
            }

            return 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int wingGoreType = Mod.Find<ModGore>("Desthroner_Wings").Type;

                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(-3, 4)), wingGoreType);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesthronerScale>(), 70, 2, 5));
        }
        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 3;
            int frameSpeed = 3;

            NPC.spriteDirection = NPC.direction;

            NPC.frameCounter += 0.5f;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
    }
}
