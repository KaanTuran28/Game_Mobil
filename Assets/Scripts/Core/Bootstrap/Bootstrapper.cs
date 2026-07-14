using System.Collections.Generic;
using GameMobil.Interfaces;
using GameMobil.Managers;
using UnityEngine;

namespace GameMobil.Core
{
    /// <summary>
    /// Composition root: tüm manager'ların oluşturulduğu, birbirine bağlandığı ve başlatıldığı
    /// tek nokta. Uygulamadaki başka hiçbir sınıf somut manager tiplerini bilmemelidir.
    /// </summary>
    /// <remarks>
    /// Başlatma sırası bağımlılık seviyesini takip eder:
    /// PoolManager -> SaveManager -> AudioManager (+ SaveManager'a kaydolur) -> UIManager
    /// -> SceneLoader -> InputManager -> GameManager -> TimeManager.
    /// AudioManager'ın SaveManager'a kaydı burada (composition root'ta) yapılır; böylece
    /// AudioManager ile SaveManager birbirini doğrudan tanımaz.
    /// </remarks>
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Sahne İçi Manager Referansları")]
        [SerializeField] private PoolManager _poolManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private InputManager _inputManager;

        private readonly List<IInitializable> _initializables = new List<IInitializable>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            RegisterServices();
            InitializeServices();
        }

        private void OnDestroy()
        {
            for (var i = _initializables.Count - 1; i >= 0; i--)
            {
                _initializables[i].Shutdown();
            }
        }

        private void RegisterServices()
        {
            var gameManager = new GameManager();
            var saveManager = new SaveManager();
            var timeManager = new TimeManager();

            GameServices.Register<IPoolManager>(_poolManager);
            GameServices.Register<ISaveManager>(saveManager);
            GameServices.Register<IAudioManager>(_audioManager);
            GameServices.Register<IUIManager>(_uiManager);
            GameServices.Register<ISceneLoader>(_sceneLoader);
            GameServices.Register<IInputManager>(_inputManager);
            GameServices.Register<IGameManager>(gameManager);
            GameServices.Register<ITimeManager>(timeManager);

            _initializables.Add(_poolManager);
            _initializables.Add(saveManager);
            _initializables.Add(_audioManager);
            _initializables.Add(_uiManager);
            _initializables.Add(_sceneLoader);
            _initializables.Add(_inputManager);
            _initializables.Add(gameManager);
            _initializables.Add(timeManager);
        }

        private void InitializeServices()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }

            // AudioManager kendini SaveManager'a kaydeder; bu bağlantı sadece composition root'ta
            // (burada) kurulur, iki manager birbirini doğrudan referans almaz.
            var saveManager = GameServices.Get<ISaveManager>();
            var audioManager = GameServices.Get<IAudioManager>();
            if (audioManager is ISaveable saveableAudioManager)
            {
                saveManager.Register(saveableAudioManager);
            }
        }
    }
}
