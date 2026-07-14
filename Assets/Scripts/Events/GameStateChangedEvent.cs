using GameMobil.Core;
using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>GameManager durum değiştirdiğinde EventBus üzerinden yayınlanır.</summary>
    public readonly struct GameStateChangedEvent : IGameEvent
    {
        public GameState PreviousState { get; }
        public GameState NewState { get; }

        public GameStateChangedEvent(GameState previousState, GameState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}
