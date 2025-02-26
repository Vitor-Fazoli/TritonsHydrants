using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Items.Materials;

namespace TritonsHydrants.Common.GlobalNPCs
{
    public class KingSlimeNPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type is NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrownFragment>(), Main.rand.Next(1, 5)));
            }
        }
    }
}