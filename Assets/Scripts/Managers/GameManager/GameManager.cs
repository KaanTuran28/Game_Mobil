using GameMobil.Core;
using GameMobil.Events;

namespace GameMobil.Managers
{
    /// <summary>
    /// Oyunun akışını (Bootstrap -> Loading -> MainMenu -> Playing -> Paused -> Win/Lose) yöneten
    /// tek sorumluluklu manager. Ses, kayıt veya UI'ın nasıl çalıştığını bilmez; sadece durum
    /// değişimini <see cref="EventBus"/> üzerinden duyurur.
    /// </summary>
    /// <remarks>
    /// Sahne referansı veya Unity yaşam döngüsüne (Update/Coroutine) ihtiyacı olmadığı için
    /// kasıtlı olarak MonoBehaviour değil, düz bir C# sınıfıdır; Bootstrapper tarafından
    /// <c>new</c> ile oluşturulup <see cref="GameServices"/> içine kaydedilir.
    /// </remarks>
    public class GameManager : IGameManager
    {
        private GameState _currentState = GameState.Bootstrap;

        public GameState CurrentState => _currentState;

        public void Initialize()
        {
            _currentState = GameState.Bootstrap;
        }

        public void Shutdown()
        {
        }

        public void ChangeState(GameState newState)
        {
            if (newState == _currentState)
            {
                return;
            }

            var previousState = _currentState;
            _currentState = newState;
            EventBus.Publish(new GameStateChangedEvent(previousState, newState));
        }
    }
}
