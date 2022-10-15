using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using DevilsWarehouse.Content.Items.Materials;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DevilsWarehouse.Common
{
    public class NPCLootModification : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            #region Powerful Enemies

            //int random = Main.rand.Next(1000);

            //if (!npc.boss)
            //{
            //    if(random >= 999)
            //    {
            //        npc.netAlways = true;
            //        npc.GivenName = "Divine " + npc.FullName;
            //        npc.damage *= 10;
            //        npc.lifeMax *= 10;
            //    }
            //    else if (random >= 900)
            //    {
            //        npc.netAlways = true;
            //        npc.GivenName = "Berserk " + npc.FullName;
            //        npc.damage *= 3;
            //        npc.color = Color.Red;
            //        npc.lifeMax /= 2;
            //    }else if (random >= 600)
            //    {
            //        npc.GivenName = "Space " + npc.FullName;
            //        npc.noGravity = true;
            //        npc.color = Color.AliceBlue;
            //    }
            //    else if (random >= 300)
            //    {
            //        npc.GivenName = "Giant " + npc.FullName;
            //        npc.scale = 3;
            //    }
            //}

            #endregion
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            #region AdamantiteFragment
            if (npc.type == NPCID.RedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AdamantiteFragment>(), 10, 1, 3));
            }
            #endregion
        }
    }
}
