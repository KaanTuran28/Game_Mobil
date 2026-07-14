using System.Collections.Generic;
using GameMobil.Core;
using GameMobil.Events;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// UI ekranlarının (panel/screen) gösterim ve gizleme akışını yöneten manager.
    /// Gerçek UI tasarımı (prefab içerikleri, layout) bu aşamada kapsam dışıdır; burada
    /// sadece kayıt/gösterme/gizleme altyapısı bulunur. GameManager'ı doğrudan tanımaz,
    /// sadece onun yayınladığı <see cref="GameStateChangedEvent"/>'i dinler.
    /// </summary>
    /// <remarks>
    /// Ekran prefab'larının altına yerleştirileceği bir kök Transform'a ihtiyaç duyduğu için
    /// MonoBehaviour olarak tasarlanmıştır.
    /// </remarks>
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Transform _screenRoot;

        private readonly Dictionary<string, GameObject> _registeredPrefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> _activeInstances = new Dictionary<string, GameObject>();

        public void Initialize()
        {
            EventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
        }

        public void Shutdown()
        {
            EventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
        }

        public void RegisterScreen(string screenId, GameObject screenPrefab)
        {
            if (string.IsNullOrEmpty(screenId) || screenPrefab == null)
            {
                return;
            }

            _registeredPrefabs[screenId] = screenPrefab;
        }

        public void ShowScreen(string screenId)
        {
            if (_activeInstances.ContainsKey(screenId))
            {
                _activeInstances[screenId].SetActive(true);
                return;
            }

            if (!_registeredPrefabs.TryGetValue(screenId, out var prefab))
            {
                Debug.LogWarning($"UIManager: '{screenId}' id'li ekran kayıtlı değil.");
                return;
            }

            var instance = Instantiate(prefab, _screenRoot);
            _activeInstances[screenId] = instance;
        }

        public void HideScreen(string screenId)
        {
            if (_activeInstances.TryGetValue(screenId, out var instance))
            {
                instance.SetActive(false);
            }
        }

        public void HideAll()
        {
            foreach (var instance in _activeInstances.Values)
            {
                instance.SetActive(false);
            }
        }

        private void OnGameStateChanged(GameStateChangedEvent evt)
        {
            // Hangi ekranın hangi GameState'te açılacağı UI tasarımı yapıldığında burada eşlenecek.
            // Bu altyapı aşamasında sadece event akışının çalıştığı doğrulanır.
        }
    }
}
