using GameMobil.Core;
using GameMobil.Events;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// <see cref="UnityEngine.Time.timeScale"/> üzerinden oyunun duraklatılıp devam ettirilmesini
    /// yönetir. GameManager'ı doğrudan tanımaz; sadece onun yayınladığı
    /// <see cref="GameStateChangedEvent"/>'e tepki verip <see cref="GameState.Paused"/>
    /// durumuna girildiğinde/çıkıldığında otomatik olarak duraklatır/devam ettirir. Bu, manager'lar
    /// arası doğrudan referans yerine EventBus ile haberleşmenin somut bir örneğidir.
    /// </summary>
    /// <remarks>
    /// Sahne referansı veya Coroutine ihtiyacı olmadığı için kasıtlı olarak MonoBehaviour değil,
    /// düz bir C# sınıfıdır.
    /// </remarks>
    public class TimeManager : ITimeManager
    {
        private float _previousTimeScale = 1f;

        public float TimeScale => Time.timeScale;

        public void Initialize()
        {
            EventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
        }

        public void Shutdown()
        {
            EventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
        }

        public void Pause()
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Time.timeScale = _previousTimeScale;
        }

        public void SetTimeScale(float scale)
        {
            Time.timeScale = Mathf.Max(0f, scale);
        }

        private void OnGameStateChanged(GameStateChangedEvent evt)
        {
            if (evt.NewState == GameState.Paused)
            {
                Pause();
            }
            else if (evt.PreviousState == GameState.Paused)
            {
                Resume();
            }
        }
    }
}
