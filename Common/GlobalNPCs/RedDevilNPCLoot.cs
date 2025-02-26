using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Items.Weapons.Tridents;

namespace TritonsHydrants.Common.GlobalNPCs
{
    public class RedDevilNPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type is NPCID.RedDevil)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnholyTrident>(), chanceDenominator: 3));
            }
        }
    }
}