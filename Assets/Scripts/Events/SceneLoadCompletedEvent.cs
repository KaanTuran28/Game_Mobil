using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>Bir sahne yüklemesi tamamlandığında yayınlanır.</summary>
    public readonly struct SceneLoadCompletedEvent : IGameEvent
    {
        public string SceneName { get; }

        public SceneLoadCompletedEvent(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}
