using GearonArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
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
                PopupText text = new PopupText();
                text.name = ("ARE YOU PREPARED ?");
                text.color = Color.Red;
                text.position = NPC.position + new Vector2(0,+50);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                PopupText text = new PopupText();
                text.name = ("ARE YOU PREPARED ?");
                text.color = Color.Red;
                text.position = NPC.position + new Vector2(0, +50);
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
           
        }
        public override void AI()
        {
            NPC.TargetClosest(faceTarget: true);
            PlayerHead();
            HealFromTheSky();

            Player player = Main.player[NPC.target];
            player.AddBuff(BuffID.CursedInferno, 1);
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
        private void HealFromTheSky()
        {
            NPC.ai[1] = 0;
            NPC.ai[1]++;

            if(NPC.ai[1] == 50)
            {
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2((NPC.Center.X - 1000) + Main.rand.Next(2000), NPC.Center.Y - 100), new Vector2(0, 10),
                    ProjectileID.EmpressBlade, NPC.damage, 5, NPC.releaseOwner);

                NPC.ai[1] = 0;
            }
        }
        private void Colunm()
        {
            //Two colunms for stuck players in specific area
            
            for (int i = 0; i < Main.player.Length; i++)
            {
                Main.player[i].position = NPC.Center + new Vector2(0, 30);
            }
            

            //Right Colunm

            //Left Colunm

        }
    }
}
