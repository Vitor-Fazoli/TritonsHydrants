using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using DevilsWarehouse.Content.Items.Materials;

namespace DevilsWarehouse.Common
{
    public class NPCLootModification : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            #region AdamantiteFragment
            if (npc.type == NPCID.RedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AdamantiteFragment>(), 10,1,3));
            }
            #endregion


        }
    }
}
