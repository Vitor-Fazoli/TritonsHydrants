using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using TritonsHydrants.Content.NPCs;

namespace TritonsHydrants.Common.Systems;

public class SirenMusicSystem : ModSystem
{
    // Background music volume during audible singing: 0 = muted, 1 = normal.
    private const float BackgroundMusicVolumeWhileSinging = 0.2f;
    private float backgroundMusicMultiplier = 1f;

    public override void Load()
    {
        if (Main.dedServ)
            return;

        On_LegacyAudioSystem.UpdateCommonTrack += UpdateCommonTrack;
        On_LegacyAudioSystem.UpdateCommonTrackTowardStopping += UpdateCommonTrackTowardStopping;
    }

    public override void Unload()
    {
        if (Main.dedServ)
            return;

        On_LegacyAudioSystem.UpdateCommonTrack -= UpdateCommonTrack;
        On_LegacyAudioSystem.UpdateCommonTrackTowardStopping -= UpdateCommonTrackTowardStopping;
    }

    public override void OnWorldUnload()
    {
        backgroundMusicMultiplier = 1f;
    }

    public override void PostUpdateEverything()
    {
        if (Main.dedServ)
            return;

        backgroundMusicMultiplier = 1f;
        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.life > 0 && npc.ModNPC is Siren siren && siren.IsSongAudible)
            {
                backgroundMusicMultiplier = BackgroundMusicVolumeWhileSinging;
                break;
            }
        }
    }

    // Scale only background tracks, preserving both the user's setting and the Siren's voice.
    private void UpdateCommonTrack(On_LegacyAudioSystem.orig_UpdateCommonTrack orig,
        LegacyAudioSystem self, bool active, int trackIndex, float totalVolume, ref float tempFade)
    {
        orig(self, active, trackIndex, totalVolume * backgroundMusicMultiplier, ref tempFade);
    }

    private void UpdateCommonTrackTowardStopping(On_LegacyAudioSystem.orig_UpdateCommonTrackTowardStopping orig,
        LegacyAudioSystem self, int trackIndex, float totalVolume, ref float tempFade, bool isMainTrackAudible)
    {
        orig(self, trackIndex, totalVolume * backgroundMusicMultiplier, ref tempFade, isMainTrackAudible);
    }
}
