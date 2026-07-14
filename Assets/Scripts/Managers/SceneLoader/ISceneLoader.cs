using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>Asenkron sahne yüklemesini yöneten manager'ın sözleşmesi.</summary>
    public interface ISceneLoader : IInitializable
    {
        /// <summary>Belirtilen sahneyi asenkron olarak yükler.</summary>
        /// <param name="sceneName">Build Settings'e eklenmiş sahne adı.</param>
        /// <param name="showLoadingScreen">Yükleme sırasında ilerleme event'lerinin yayınlanıp yayınlanmayacağı.</param>
        void LoadScene(string sceneName, bool showLoadingScreen = true);
    }
}
