using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Master ses seviyesinin değiştirilmesi istendiğinde yayınlanır (ör. ayarlar ekranından).</summary>
    public readonly struct SetMasterVolumeEvent : IGameEvent
    {
        public float Volume { get; }

        public SetMasterVolumeEvent(float volume)
        {
            Volume = volume;
        }
    }
}
