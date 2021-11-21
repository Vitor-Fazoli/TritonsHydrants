using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using GearonArsenalMod.Content.NPCs.Enemies;

namespace GearonArsenalMod
{
    class ModGlobalNPC : GlobalNPC{

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){

            //#region Broken Hammer
            //if (npc.type == NPCID.EyeofCthulhu)
            //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHammer>(),1));
            //#endregion

        }

        public override bool CheckDead(NPC npc)
        {
            if (npc.type == NPCID.BloodZombie && npc.life <= 0)
            {
                Random rand = new Random();
                
                for(int i = 0; i < rand.Next(0, 4); i++)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<BloodWorm>());
                }
            }
                return true;
        }
    }
}
