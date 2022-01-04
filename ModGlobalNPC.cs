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
using GearonArsenalMod.Content.Item.Accessories;

namespace GearonArsenalMod {
    class ModGlobalNPC : GlobalNPC {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {

            //#region Broken Hammer
            //if (npc.type == NPCID.EyeofCthulhu)
            //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHammer>(),1));
            //#endregion

        }

        public override bool CheckDead(NPC npc) {

            //new mob to spawn on blood zombie dead
            if (npc.type == NPCID.BloodZombie && npc.life <= 0) {
                Random rand = new Random();

                for (int i = 0; i < rand.Next(0, 2); i++) {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<BloodWorm>());
                }
            }
            return true;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            //Adding new itens to Dryad shop
            if (type == NPCID.Dryad) {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Balm>());
            }
        }
    }
}
