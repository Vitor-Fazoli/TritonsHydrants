using DevilsWarehouse.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common
{
    public class NPCLootModification : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            #region AdamantiteFragment
            if (npc.type == NPCID.RedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AdamantiteFragment>(), 10, 1, 3));
            }
            #endregion

            #region GraniteEye
            if (npc.type == NPCID.GraniteFlyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AdamantiteFragment>(), 1000, 1, 1));
            }
            #endregion
        }
    }
}
