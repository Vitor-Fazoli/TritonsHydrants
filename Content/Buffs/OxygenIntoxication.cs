using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class OxygenIntoxication : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Helper.ToDisplay(Name));
        }
        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OxygenIntoxicationPlayer>().OxygenIntoxication = true;
        }
    }
    public class OxygenIntoxicationPlayer : ModPlayer
    {
        public bool OxygenIntoxication;

        public override void ResetEffects()
        {
            OxygenIntoxication = false;
        }
        public override void UpdateDead()
        {
            OxygenIntoxication = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (OxygenIntoxication)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 1;
            }

        }
    }

}
