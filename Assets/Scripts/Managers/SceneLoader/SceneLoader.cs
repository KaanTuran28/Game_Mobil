using System.Collections;
using GameMobil.Core;
using GameMobil.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMobil.Managers
{
    /// <summary>
    /// Asenkron sahne yüklemesini yönetir ve ilerlemeyi <see cref="EventBus"/> üzerinden
    /// yayınlar; bir loading ekranı (henüz tasarlanmadı) bu event'lere abone olarak
    /// ilerleme çubuğunu güncelleyebilir.
    /// </summary>
    /// <remarks>
    /// Unity'nin <see cref="SceneManager.LoadSceneAsync"/> ilerlemesini bir Coroutine ile takip
    /// ettiği için MonoBehaviour olarak tasarlanmıştır.
    /// </remarks>
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private const float SceneActivationThreshold = 0.9f;

        private bool _isLoading;

        public void Initialize()
        {
            EventBus.Subscribe<SceneLoadRequestEvent>(OnSceneLoadRequested);
        }

        public void Shutdown()
        {
            EventBus.Unsubscribe<SceneLoadRequestEvent>(OnSceneLoadRequested);
        }

        public void LoadScene(string sceneName, bool showLoadingScreen = true)
        {
            if (_isLoading)
            {
                Debug.LogWarning("SceneLoader: Zaten bir yükleme sürüyor, yeni istek yok sayıldı.");
                return;
            }

            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private void OnSceneLoadRequested(SceneLoadRequestEvent evt) => LoadScene(evt.SceneName, evt.ShowLoadingScreen);

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            _isLoading = true;

            var operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (operation.progress < SceneActivationThreshold)
            {
                EventBus.Publish(new SceneLoadProgressEvent(operation.progress));
                yield return null;
            }

            EventBus.Publish(new SceneLoadProgressEvent(1f));
            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                yield return null;
            }

            _isLoading = false;
            EventBus.Publish(new SceneLoadCompletedEvent(sceneName));
        }
    }
}
