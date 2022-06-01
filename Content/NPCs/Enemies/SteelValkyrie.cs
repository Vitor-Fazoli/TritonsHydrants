using GearonArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace GearonArsenal.Content.NPCs.Enemies
{
    public class SteelValkyrie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            DisplayName.SetDefault("Steel Valkyrie");

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Position = new Vector2(0, 100)
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 124;
            NPC.height = 174;
            NPC.boss = true;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.AbigailCry;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.defense = 20;
            NPC.knockBackResist = 0;
            NPC.netAlways = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 10f;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/KnightsOfCydonia");
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += .25f;
            NPC.frameCounter %= 10.0;

            int frame = (int)(NPC.frameCounter / 2.0);
            NPC.frame.Y = frame * frameHeight;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("brainwashed to exterminate those responsible for the death of the wall of flesh")
            });

        public override void Load()
        {

        }
        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath15, NPC.position);
            }			

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(NPC.GivenName + "FUCK YOU" + Lang.misc[16],Color.AliceBlue);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral("Fuck you"));
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
           
        }
        public override void AI()
        {
            NPC.TargetClosest(faceTarget: true);
            Player player = Main.player[NPC.target];

            Vector2 distance = player.Center - NPC.Center;

            float random = Main.rand.Next(100);

            if(distance.X >= 300 + random || distance.X <= -300 - random || distance.Y >= 300 + random || distance.Y <= -300 - random)
            {
                SpeedDistance(distance.X);
            }
            else
            {
                PlayerHead();
            }
        }

        //combat tatics

        private void PlayerHead()
        {
            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center + new Vector2(0f, -200f);

            float speed = 7f;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 10f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }

        private void SpeedDistance(float dist)
        {
            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center;
            if (NPC.ai[0] <= 0f)
            {
                float speed = 20f + (1.01f * dist);
                Vector2 move = moveTo - NPC.Center;
                float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 10f; //the larger this is, the slower the npc will turn
                move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
                magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                NPC.velocity = move;
            }
            NPC.ai[0] -= 1f;
        }
    }
}
