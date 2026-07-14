using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>Oyunun zaman akışını (Time.timeScale) yöneten manager'ın sözleşmesi.</summary>
    public interface ITimeManager : IInitializable
    {
        float TimeScale { get; }
        void Pause();
        void Resume();
        void SetTimeScale(float scale);
    }
}
