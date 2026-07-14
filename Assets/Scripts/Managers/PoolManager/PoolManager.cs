using System.Collections.Generic;
using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// Birden fazla prefab tipi için <see cref="Pool{T}"/> örneklerini tutan ve dışarıya
    /// tek bir arayüz (<see cref="IPoolManager"/>) üzerinden sunan facade.
    /// </summary>
    /// <remarks>
    /// Havuzlanan nesnelerin sahnede bir parent Transform altında tutulması gerektiği ve
    /// (ileride) coroutine tabanlı bakım işleri (ör. otomatik boşaltma) barındırabileceği için
    /// MonoBehaviour olarak tasarlanmıştır.
    /// </remarks>
    public class PoolManager : MonoBehaviour, IPoolManager
    {
        [SerializeField] private Transform _poolContainer;

        // Prefab InstanceID -> Pool<T> (kapalı generic tip için object olarak saklanır, kullanım noktasında geri cast edilir).
        private readonly Dictionary<int, object> _poolsByPrefabId = new Dictionary<int, object>();

        // Aktif örnek -> ait olduğu Pool<T>. Release sırasında doğru havuzu bulmak için kullanılır.
        private readonly Dictionary<Component, object> _poolByActiveInstance = new Dictionary<Component, object>();

        public void Initialize()
        {
            if (_poolContainer == null)
            {
                _poolContainer = new GameObject("PoolContainer").transform;
                _poolContainer.SetParent(transform);
            }
        }

        public void Shutdown()
        {
            _poolsByPrefabId.Clear();
            _poolByActiveInstance.Clear();
        }

        public T Get<T>(T prefab) where T : Component, IPoolable
        {
            if (prefab == null)
            {
                Debug.LogWarning("PoolManager: null prefab için Get() çağrıldı.");
                return null;
            }

            var prefabId = prefab.GetInstanceID();
            if (!_poolsByPrefabId.TryGetValue(prefabId, out var poolObj))
            {
                poolObj = new Pool<T>(prefab, _poolContainer);
                _poolsByPrefabId[prefabId] = poolObj;
            }

            var pool = (Pool<T>)poolObj;
            var instance = pool.Get();
            _poolByActiveInstance[instance] = pool;
            return instance;
        }

        public void Release<T>(T instance) where T : Component, IPoolable
        {
            if (instance == null)
            {
                return;
            }

            if (_poolByActiveInstance.TryGetValue(instance, out var poolObj))
            {
                ((Pool<T>)poolObj).Release(instance);
                _poolByActiveInstance.Remove(instance);
            }
            else
            {
                Debug.LogWarning($"PoolManager: '{instance.name}' bu PoolManager üzerinden alınmamış, yok ediliyor.");
                Destroy(instance.gameObject);
            }
        }
    }
}
