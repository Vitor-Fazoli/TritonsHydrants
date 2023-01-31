using DevilsWarehouse.Content.Items;
using DevilsWarehouse.Content.NPCs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DevilsWarehouse.Common.Systems
{
    public class StarSystem : GlobalItem
    {
        public const int starMax = 7;
        public int starCurrent = 0;

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.damage > 0;
        }
        public override void OnCreate(Item item, ItemCreationContext context)
        {
            starCurrent = Main.rand.Next(starMax);
            Mod.Logger.Warn(starCurrent);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            damage += 0.1f * starCurrent;
        }
        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            if (starCurrent >= starMax)
            {
                crit += 0.1f;
            }
        }

        #region Net Update
        public override void LoadData(Item item, TagCompound tag)
        {
            starCurrent = tag.GetInt("starCurrent");
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["starCurrent"] = starCurrent;
        }
        #endregion

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            var sb = new System.Text.StringBuilder();

            int emptyStar = starMax - starCurrent;

            for (int i = 0; i < starMax; i++)
            {
                if (i > starCurrent)
                {
                    sb.Append($"[i:{ItemID.FallenStar}] ");
                }
                else
                {
                    sb.Append($"[i:{ItemID.ManaCrystal}] ");
                }
            }

            var line = new TooltipLine(Mod, "Face", sb.ToString());
            tooltips.Add(line);
        }
    }
}
