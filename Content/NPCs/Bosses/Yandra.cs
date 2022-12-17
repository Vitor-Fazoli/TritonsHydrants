using System;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.NPCs.Bosses
{
    public class Yandra : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 200;
            NPC.lifeMax = 10000;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 20;
            NPC.boss = true;
            NPC.knockBackResist = 1f;
        }
        public override bool PreAI()
        {
            return base.PreAI();
        }
        public override void AI()
        {
            NPC.TargetClosest(true);

            NPC.ai[0]++;

            if (NPC.ai[0] == 100)
            {
                SpearDash();
                NPC.ai[0] = 0;
            }
        }
        public override void PostAI()
        {
            base.PostAI();
        }
 
        private Player isTarget()
        {
            Player p = Main.player[NPC.target];

            return p;
        }
        private double TargetsPower(Player target)
        {
            double strength = 0;

            if (target.statDefense > 10 && target.statLifeMax2 >= 300)
            {
                strength += 0.05 + Main.rand.NextFloat(0.03f);
            }
            else if (target.statDefense > 40 || target.statLifeMax2 >= 500)
            {
                strength += 0.4 + Main.rand.NextFloat(0.05f);
            }
            else if (target.statDefense >= 60 || target.statLifeMax2 >= 600)
            {
                strength += 0.7 + Main.rand.NextFloat(0.05f);
            }
            else
            {
                strength += 0.01f;
            }

            if (target.getDPS() >= 1000)
            {
                strength += 0.05 + Main.rand.NextFloat(0.08f);
            }
            else if (target.getDPS() >= 2000)
            {
                strength += 0.1 + Main.rand.NextFloat(0.07f);
            }
            else if (target.getDPS() >= 5000)
            {
                strength += 0.2 + Main.rand.NextFloat(0.03f);
            }
            else if (target.getDPS() >= 10000)
            {
                strength += 0.5 + Main.rand.NextFloat(0.02f);
            }
            else
            {
                strength += 0.01f;
            }

            return strength;
        }

        private bool TargetRunningOut(Player target)
        {
            if ((target.position.X - NPC.position.X) > 600 * TargetsPower(target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Attacks - Movements
        private void SpearDash()
        {
            Player p = isTarget();

            if (NPC.position.Y == p.position.Y + (60 - Main.rand.Next(120)))
            {
                NPC.position.X += 2000 * p.direction;
            }           
        }
        private void BlockProjectiles()
        {

        }
        private void ArrowVolley()
        {

        }
        private void SpearThrow()
        {

        }
        private void UsingHerbs()
        {

        }
    }
}
