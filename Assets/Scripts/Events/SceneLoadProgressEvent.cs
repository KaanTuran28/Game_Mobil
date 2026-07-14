using GameMobil.Interfaces;

namespace GameMobil.Events
{
    /// <summary>SceneLoader, asenkron yükleme ilerlemesini bu event ile yayınlar; bir loading ekranı buna abone olabilir.</summary>
    public readonly struct SceneLoadProgressEvent : IGameEvent
    {
        /// <summary>0-1 aralığında yükleme ilerlemesi.</summary>
        public float Progress { get; }

        public SceneLoadProgressEvent(float progress)
        {
            Progress = progress;
        }
    }
}
