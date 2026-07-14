using GameMobil.Core;
using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>Oyunun üst seviye akış durumunu yöneten manager'ın sözleşmesi.</summary>
    public interface IGameManager : IInitializable
    {
        /// <summary>Güncel oyun durumu.</summary>
        GameState CurrentState { get; }

        /// <summary>Oyun durumunu değiştirir ve <see cref="GameMobil.Events.GameStateChangedEvent"/> yayınlar.</summary>
        void ChangeState(GameState newState);
    }
}
