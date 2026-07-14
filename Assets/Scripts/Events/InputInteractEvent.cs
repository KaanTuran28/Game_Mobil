using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>InputManager, etkileşim girdisi algılandığında yayınlar. Gameplay tarafı bu event'e abone olur.</summary>
    public readonly struct InputInteractEvent : IGameEvent
    {
    }
}
