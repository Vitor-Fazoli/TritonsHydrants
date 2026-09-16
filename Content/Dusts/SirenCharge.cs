using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TritonsHydrants.Content.NPCs;

namespace TritonsHydrants.Content.Dusts;

public class SirenCharge : ModDust
{
    public override string Texture => "TritonsHydrants/Content/Dusts/ArcanePowder";

    public override void OnSpawn(Dust dust)
    {
        dust.noGravity = true;
        dust.scale = 0.8f;
        dust.fadeIn = 0f;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor) => Color.Cyan;

    public override bool Update(Dust dust)
    {
        if (dust.customData is not NPC npc || !npc.active || npc.ModNPC is not Siren siren || npc.ai[0] != 2f)
        {
            dust.active = false;
            return false;
        }

        Vector2 offset = siren.TridentTip - dust.position;
        if (offset.LengthSquared() <= 9f || ++dust.fadeIn >= 20f)
        {
            dust.active = false;
            return false;
        }

        dust.velocity = offset.SafeNormalize(Vector2.Zero) * 3f;
        dust.position += dust.velocity;
        dust.scale *= 0.97f;
        return false;
    }
}
