using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>InputManager, saldırı girdisi algılandığında yayınlar. Gameplay tarafı bu event'e abone olur.</summary>
    public readonly struct InputAttackEvent : IGameEvent
    {
    }
}
