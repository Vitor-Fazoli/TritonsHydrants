using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common.Systems;
using TritonsHydrants.Content.NPCs;

namespace TritonsHydrants;

public class TritonsHydrants : Mod
{
    internal const byte SetSirenSinging = 0;

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        byte message = reader.ReadByte();
        if (message != SetSirenSinging)
            return;

        int npcIndex = reader.ReadInt16();
        bool enabled = reader.ReadBoolean();
        if (Main.netMode != NetmodeID.Server || whoAmI < 0 || whoAmI >= Main.maxPlayers ||
            npcIndex < 0 || npcIndex >= Main.maxNPCs)
            return;

        Player player = Main.player[whoAmI];
        NPC npc = Main.npc[npcIndex];
        if (player.active && !player.dead && npc.active && npc.ModNPC is Siren &&
            Vector2.DistanceSquared(player.Center, npc.Center) <= 400f * 400f)
            SirenSingingSystem.SetSingingEnabled(enabled);
    }
}
