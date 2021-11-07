using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenalMod
{
    class ModGlobalNPC : GlobalNPC{

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){

            //#region Broken Hammer
            //if (npc.type == NPCID.EyeofCthulhu)
            //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHammer>(),1));
            //#endregion

        }
    }
}
