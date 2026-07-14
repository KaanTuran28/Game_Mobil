using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>InputManager, zıplama girdisi algılandığında yayınlar. Gameplay tarafı bu event'e abone olur.</summary>
    public readonly struct InputJumpEvent : IGameEvent
    {
    }
}
