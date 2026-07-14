using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>Müzik/SFX çalma, master ses seviyesi ve sessize alma işlevlerini yöneten manager'ın sözleşmesi.</summary>
    public interface IAudioManager : IInitializable
    {
        float MasterVolume { get; }
        bool IsMuted { get; }

        void PlayMusic(string trackId);
        void StopMusic();
        void PlaySfx(string clipId);
        void SetMasterVolume(float volume);
        void SetMute(bool isMuted);
    }
}
