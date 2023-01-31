using DevilsWarehouse.Content.NPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles
{
    internal class FightsabreProj : ModProjectile
    {
        public override void OnSpawn(IEntitySource source)
        {
            NPC.NewNPCDirect(source, (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<FightsabreReflect>());
            Projectile.Kill();
        }
    }
}
