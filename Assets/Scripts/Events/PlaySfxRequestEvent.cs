using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Gameplay/UI kodu, AudioManager'ı doğrudan çağırmak yerine bu event'i yayınlayarak bir SFX çalınmasını ister.</summary>
    public readonly struct PlaySfxRequestEvent : IGameEvent
    {
        public string ClipId { get; }

        public PlaySfxRequestEvent(string clipId)
        {
            ClipId = clipId;
        }
    }
}
