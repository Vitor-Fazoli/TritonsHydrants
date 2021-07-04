using Terraria;
using GreatswordsMod.Abstract;

namespace GreatswordsMod.Attack
{
    public class SilverSlash : Slash
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Slash");
            Main.projFrames[projectile.type] = frames;
        }
        public override void SetDefaults()
        {
            //properties - Default
            projectile.width = 200;
            projectile.height = 200;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;

            //properties - Slash
            frames = 5;
            spdFrame = 4;
        }
    }
}
