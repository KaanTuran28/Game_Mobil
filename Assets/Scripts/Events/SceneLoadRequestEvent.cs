using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Bir sahnenin asenkron olarak yüklenmesi istendiğinde yayınlanır.</summary>
    public readonly struct SceneLoadRequestEvent : IGameEvent
    {
        public string SceneName { get; }
        public bool ShowLoadingScreen { get; }

        public SceneLoadRequestEvent(string sceneName, bool showLoadingScreen = true)
        {
            SceneName = sceneName;
            ShowLoadingScreen = showLoadingScreen;
        }
    }
}
