using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Gameplay/UI kodu, AudioManager'ı doğrudan çağırmak yerine bu event'i yayınlayarak müzik çalınmasını ister.</summary>
    public readonly struct PlayMusicRequestEvent : IGameEvent
    {
        public string TrackId { get; }

        public PlayMusicRequestEvent(string trackId)
        {
            TrackId = trackId;
        }
    }
}
