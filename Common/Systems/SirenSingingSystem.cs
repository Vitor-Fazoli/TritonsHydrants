using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TritonsHydrants.Content.NPCs;

namespace TritonsHydrants.Common.Systems;

public class SirenSingingSystem : ModSystem
{
    public static bool SingingEnabled { get; private set; } = true;

    public override void ClearWorld() => SingingEnabled = true;

    public override void SaveWorldData(TagCompound tag)
    {
        tag["SirenSingingDisabled"] = !SingingEnabled;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        SingingEnabled = !tag.GetBool("SirenSingingDisabled");
    }

    public override void NetSend(BinaryWriter writer) => writer.Write(SingingEnabled);

    public override void NetReceive(BinaryReader reader)
    {
        SingingEnabled = reader.ReadBoolean();
        ApplySetting();
    }

    internal static void SetSingingEnabled(bool enabled)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient || SingingEnabled == enabled)
            return;

        SingingEnabled = enabled;
        ApplySetting();
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.WorldData);
    }

    private static void ApplySetting()
    {
        foreach (NPC npc in Main.ActiveNPCs)
            if (npc.ModNPC is Siren siren)
                siren.OnSingingSettingChanged();
    }
}
