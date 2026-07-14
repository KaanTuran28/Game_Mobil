using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>
    /// SaveManager yükleme akışını tamamladığında yayınlanır.
    /// Not: Şu an gerçek bir dosya/JSON kalıcılığı olmadığı için bu event sadece akış tamamlandı bilgisini taşır.
    /// </summary>
    public readonly struct LoadCompletedEvent : IGameEvent
    {
    }
}
