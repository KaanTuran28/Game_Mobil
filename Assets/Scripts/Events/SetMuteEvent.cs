using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Tüm sesin sessize alınması/açılması istendiğinde yayınlanır.</summary>
    public readonly struct SetMuteEvent : IGameEvent
    {
        public bool IsMuted { get; }

        public SetMuteEvent(bool isMuted)
        {
            IsMuted = isMuted;
        }
    }
}
