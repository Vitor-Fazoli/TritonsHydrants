using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using TritonsHydrants.Common.Systems;
using TritonsHydrants.Content.Dusts;
using TritonsHydrants.Content.Projectiles;
using TritonsHydrants.Utils;
using XPT.Core.Audio.MP3Sharp;

namespace TritonsHydrants.Content.NPCs;

[AutoloadHead]
public class Siren : ModNPC
{
    private static Profiles.DefaultNPCProfile NPCProfile;

    private enum SirenState
    {
        Idle,
        Singing,
        Attacking
    }

    // Singing test settings. Times are in ticks (60 ticks = 1 second).
    // Set UseFixedSingingDelay to false to restore the happiness-based delay.
    private const bool UseFixedSingingDelay = true;
    private const int FixedSingingDelay = 5 * 60;
    private const int NoteInterval = 20; // Must be at least 1.
    private const int SingingFrameDuration = 8; // Must be at least 1.
    private const float NoteOffsetX = -4f;
    private const float NoteScale = 1.5f;
    private const float ChatBubbleOffsetX = -6f;

    private const string SongAsset = "Common/Assets/Musics/SirenSong";
    private static readonly SoundStyle SingingSound = new("TritonsHydrants/" + SongAsset, SoundType.Music)
    {
        IsLooped = false,
        MaxInstances = 0,
        PauseBehavior = PauseBehavior.PauseWithGame
    };
    private static int singingDuration;
    private SlotId singingSoundSlot;
    private bool startedSingingSound;

    internal bool IsSongAudible => SirenSingingSystem.SingingEnabled && CurrentState == SirenState.Singing && startedSingingSound &&
        SoundEngine.TryGetActiveSound(singingSoundSlot, out var song) &&
        song.IsPlayingOrPaused && song.Sound.Volume > 0f;

    private const int AttackFrameDuration = 6;
    private const int AttackDuration = 16 * AttackFrameDuration;
    private const float AttackRange = 600f;
    private const int MinimumHouseInteriorHeight = 10;

    private int AttackFrame => 6 + Math.Min((int)NPC.ai[1] / AttackFrameDuration, 15);

    // ai[2] stores the attack target's index plus one; zero means no target.
    // ai[0] stores the current state; ai[1] counts ticks in that state.
    private SirenState CurrentState => (SirenState)NPC.ai[0];

    private Vector2 MouthPosition => NPC.Top + new Vector2(NPC.spriteDirection * 8f, 32f + NPC.gfxOffY);

    public override void Load()
    {
        double durationSeconds;
        if (Main.dedServ)
        {
            // Match tModLoader's MP3 reader: decoded PCM is 16-bit stereo.
            // A dedicated server must measure the song without creating an audio device.
            using var mp3 = new MP3Stream(Mod.GetFileStream(SongAsset + ".mp3"));
            byte[] buffer = new byte[8192];
            long decodedBytes = 0;
            int bytesRead;
            while ((bytesRead = mp3.Read(buffer, 0, buffer.Length)) > 0)
                decodedBytes += bytesRead;
            durationSeconds = decodedBytes / (mp3.Frequency * 4d);
        }
        else
        {
            durationSeconds = ModContent.Request<SoundEffect>("TritonsHydrants/" + SongAsset,
                ReLogic.Content.AssetRequestMode.ImmediateLoad).Value.Duration.TotalSeconds;
        }

        singingDuration = Math.Max(1, (int)Math.Ceiling(durationSeconds * 60d));
    }

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 22;
        NPCID.Sets.DangerDetectRange[Type] = 0;
        NPCID.Sets.PrettySafe[Type] = 300;
        NPCID.Sets.ShimmerTownTransform[NPC.type] = false;

        NPC.Happiness
            .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
            .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
            .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
            .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like)
            .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate);

        NPCID.Sets.SpawnsWithCustomName[Type] = true;

        NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
        {
            Velocity = 0f,
            Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
        };

        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        NPCProfile = new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture));
    }

    public override void SetDefaults()
    {
        NPC.townNPC = true;
        NPC.friendly = true;
        // Keep the custom singing/attack states and sprite frames instead of town NPC AI.
        NPC.aiStyle = -1;
        NPC.width = 75;
        NPC.height = 90;
        NPC.damage = 1;
        NPC.defense = 15;
        NPC.lifeMax = 1000;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0f;
        NPC.direction = -1;
    }

    public override bool CanChat()
    {
        return true;
    }

    public override void ChatBubblePosition(ref Vector2 position, ref SpriteEffects spriteEffects)
    {
        position = MouthPosition - Main.screenPosition + new Vector2(
            NPC.spriteDirection * 12f + ChatBubbleOffsetX - TextureAssets.Chat.Value.Width / 2f,
            -TextureAssets.Chat.Value.Height);
        spriteEffects = NPC.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange([
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

                new FlavorTextBestiaryInfoElement("Mods.TritonsHydrants.Bestiary.Siren"),
            ]);
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (hit.Damage > 0 && CurrentState == SirenState.Singing && Main.netMode != NetmodeID.MultiplayerClient)
        {
            // Receiving damage interrupts the song; the next AI tick can select an attack target.
            ChangeState(SirenState.Idle);
            UpdateSingingSound();
        }

        SoundEngine.PlaySound(SoundID.AbigailCry, Entity.position);

        int num = NPC.life > 0 ? 1 : 5;

        for (int k = 0; k < num; k++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BloodWater);
        }
    }

    public override void AI()
    {
        NPC.ai[1]++;

        if (CurrentState == SirenState.Idle && Main.netMode != NetmodeID.MultiplayerClient)
        {
            int targetIndex = FindAttackTarget();
            if (targetIndex >= 0)
            {
                NPC.ai[2] = targetIndex + 1;
                NPC.direction = NPC.spriteDirection = Main.npc[targetIndex].Center.X < NPC.Center.X ? -1 : 1;
                ChangeState(SirenState.Attacking);
            }
        }

        NPC.spriteDirection = NPC.direction;

        switch (CurrentState)
        {
            case SirenState.Idle:
                if (SirenSingingSystem.SingingEnabled && Main.netMode != NetmodeID.MultiplayerClient &&
                    (UseFixedSingingDelay || NPC.ai[1] % 60 == 0))
                {
                    int playerIndex = Player.FindClosest(NPC.position, NPC.width, NPC.height);
                    Player listener = Main.player[playerIndex];
                    int singingDelay = UseFixedSingingDelay ? FixedSingingDelay : GetSingingDelay(listener);
                    if (listener.active && !listener.dead && NPC.ai[1] >= singingDelay)
                        ChangeState(SirenState.Singing);
                }
                break;

            case SirenState.Attacking:
                UpdateAttack();
                break;

            case SirenState.Singing:
                if (!Main.dedServ && NPC.ai[1] % NoteInterval == 0)
                {
                    Vector2 mouthPosition = MouthPosition + new Vector2(NoteOffsetX, 0f);
                    mouthPosition += new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                    Dust.NewDustPerfect(mouthPosition, ModContent.DustType<Notes>(), Scale: NoteScale);
                }

                // The server uses the file's duration; single player also waits for playback.
                if (NPC.ai[1] >= singingDuration &&
                    (Main.dedServ || !SoundEngine.TryGetActiveSound(singingSoundSlot, out var song) || !song.IsPlayingOrPaused))
                {
                    ChangeState(SirenState.Idle);
                }
                break;
        }

        UpdateSingingSound();
    }

    private void UpdateSingingSound()
    {
        if (Main.dedServ)
            return;

        if (!SirenSingingSystem.SingingEnabled || CurrentState != SirenState.Singing)
        {
            if (startedSingingSound && SoundEngine.TryGetActiveSound(singingSoundSlot, out var song))
                song.Stop();
            startedSingingSound = false;
            return;
        }

        if (startedSingingSound)
            return;

        startedSingingSound = true;
        singingSoundSlot = SoundEngine.PlaySound(SingingSound, MouthPosition, sound =>
        {
            sound.Position = MouthPosition;
            return NPC.active && NPC.life > 0 && SirenSingingSystem.SingingEnabled &&
                CurrentState == SirenState.Singing && !Main.gameMenu;
        });
    }

    internal void OnSingingSettingChanged()
    {
        // Restart the waiting period when toggled, without interrupting combat.
        if (CurrentState != SirenState.Attacking)
            ChangeState(SirenState.Idle);
        UpdateSingingSound();
    }

    public override void FindFrame(int frameHeight)
    {
        int frame = CurrentState switch
        {
            SirenState.Singing => 1 + (int)(NPC.ai[1] / SingingFrameDuration) % 5,
            SirenState.Attacking => AttackFrame,
            _ => 0
        };

        // The sheet has 130-pixel frames with two transparent rows between them.
        NPC.frame = new Rectangle(0, frame * 132, 72, 130);
    }

    public Vector2 TridentTip => NPC.Bottom + new Vector2(
        -NPC.spriteDirection * 14f, -130f + (AttackFrame >= 20 ? 18f : 4f) + NPC.gfxOffY);

    private int FindAttackTarget()
    {
        int targetIndex = -1;
        float nearestDistance = AttackRange * AttackRange;
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC target = Main.npc[i];
            if (!target.CanBeChasedBy(NPC))
                continue;

            float distance = Vector2.DistanceSquared(NPC.Center, target.Center);
            if (distance < nearestDistance && Collision.CanHitLine(NPC.position, NPC.width, NPC.height,
                target.position, target.width, target.height))
            {
                nearestDistance = distance;
                targetIndex = i;
            }
        }
        return targetIndex;
    }

    private void UpdateAttack()
    {
        if (!Main.dedServ && AttackFrame >= 9 && AttackFrame <= 20)
        {
            Vector2 tip = TridentTip;
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = tip + Main.rand.NextVector2CircularEdge(28f, 28f);
                Vector2 velocity = (tip - position).SafeNormalize(Vector2.Zero) * 3f;
                Dust dust = Dust.NewDustPerfect(position, TritonsDusts.GetWaterDust(), velocity,
                    newColor: Water.GetColor(), Scale: Main.rand.NextFloat(0.9f, 1.2f));
                dust.noGravity = true;
            }

            Lighting.AddLight(tip, 0f, 0.5f, 0.65f);
        }

        // Fire once, on the first tick of frame 20.
        if (NPC.ai[1] == (20 - 6) * AttackFrameDuration && Main.netMode != NetmodeID.MultiplayerClient)
        {
            int targetIndex = (int)NPC.ai[2] - 1;
            if (targetIndex < 0 || targetIndex >= Main.maxNPCs || !Main.npc[targetIndex].CanBeChasedBy(NPC))
                targetIndex = FindAttackTarget();

            if (targetIndex >= 0)
            {
                Vector2 velocity = (Main.npc[targetIndex].Center - TridentTip).SafeNormalize(Vector2.UnitY) * 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), TridentTip, velocity,
                    ModContent.ProjectileType<SirenAquaticArrow>(), 20, 2f, Main.myPlayer, targetIndex + 1);
            }
        }

        if (NPC.ai[1] >= AttackDuration)
        {
            ChangeState(SirenState.Idle);
        }
    }

    private void ChangeState(SirenState state)
    {
        // The server controls transitions and synchronizes both AI slots.
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }

        NPC.ai[0] = (float)state;
        NPC.ai[1] = 0f;
        NPC.netUpdate = true;
    }

    public override ITownNPCProfile TownNPCProfile()
    {
        return NPCProfile;
    }

    public override bool CanTownNPCSpawn(int numTownNPCs)
    {
        return NPC.downedSlimeKing;
    }

    public override bool CheckConditions(int left, int right, int top, int bottom)
    {
        // Terraria supplies inclusive interior bounds; solid floor/ceiling tiles are already excluded.
        int interiorHeight = bottom - top + 1;
        return interiorHeight >= MinimumHouseInteriorHeight;
    }

    public override List<string> SetNPCNameList()
    {
        return
        [
            "Victrimeire",
            "Seline",
            "Lyra",
        ];
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return 0f;
    }

    public override string GetChat()
    {
        WeightedRandom<string> chat = new();

        const string prefix = "Mods.TritonsHydrants.Dialogue.Siren.";
        for (int i = 1; i <= 6; i++)
            chat.Add(Language.GetTextValue(prefix + "StandardDialogue" + i));

        double priceAdjustment = Main.ShopHelper.GetShoppingSettings(Main.LocalPlayer, NPC).PriceAdjustment;
        string mood = priceAdjustment <= 0.85 ? "Happy" : priceAdjustment >= 1.05 ? "Unhappy" : "Shy";
        for (int i = 1; i <= 3; i++)
            chat.Add(Language.GetTextValue(prefix + mood + "Dialogue" + i), 2);

        return chat;
    }

    private int GetSingingDelay(Player listener)
    {
        // Use Terraria's happiness calculation, including housing, crowding and neighbors.
        // Like the happiness dialogue, biome preferences use the listening player's biome.
        double priceAdjustment = Main.ShopHelper.GetShoppingSettings(listener, NPC).PriceAdjustment;
        if (priceAdjustment <= 0.85)
            return 15 * 60;
        if (priceAdjustment <= 0.95)
            return 45 * 60;
        if (priceAdjustment < 1.05)
            return 90 * 60;

        return 180 * 60;
    }

    public override void SetChatButtons(ref string button, ref string button2)
    {
        button = Language.GetTextValue("LegacyInterface.28");
        button2 = Language.GetTextValue("Mods.TritonsHydrants.Dialogue.Siren." +
            (SirenSingingSystem.SingingEnabled ? "DisableSinging" : "EnableSinging"));
    }

    public override void OnChatButtonClicked(bool firstButton, ref string shop)
    {
        if (firstButton)
        {
            shop = "Shop";
        }
        else if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write(global::TritonsHydrants.TritonsHydrants.SetSirenSinging);
            packet.Write((short)NPC.whoAmI);
            packet.Write(!SirenSingingSystem.SingingEnabled);
            packet.Send();
        }
        else
        {
            SirenSingingSystem.SetSingingEnabled(!SirenSingingSystem.SingingEnabled);
        }
    }

    public override void AddShops()
    {
        new NPCShop(Type)
            .Add(ItemID.SharkFin)
            .Register();
    }
}
