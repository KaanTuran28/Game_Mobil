using System.Collections.Generic;
using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// Tek bir prefab tipi için generic nesne havuzu. <see cref="Object.Instantiate"/> ve
    /// <see cref="Object.Destroy"/> çağrılarını (mobilde en yaygın GC/CPU maliyet kaynaklarından biri)
    /// minimuma indirmek için kullanılır.
    /// </summary>
    /// <typeparam name="T">Havuzlanacak bileşen tipi.</typeparam>
    public class Pool<T> where T : Component, IPoolable
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _inactiveInstances = new Stack<T>();

        public Pool(T prefab, Transform parent, int prewarmCount = 0)
        {
            _prefab = prefab;
            _parent = parent;

            for (var i = 0; i < prewarmCount; i++)
            {
                var instance = CreateInstance();
                instance.gameObject.SetActive(false);
                _inactiveInstances.Push(instance);
            }
        }

        /// <summary>Havuzdan aktif bir örnek alır; havuz boşsa yeni bir örnek oluşturur.</summary>
        public T Get()
        {
            var instance = _inactiveInstances.Count > 0 ? _inactiveInstances.Pop() : CreateInstance();
            instance.gameObject.SetActive(true);
            instance.OnSpawn();
            return instance;
        }

        /// <summary>Bir örneği pasifleştirip havuza geri koyar.</summary>
        public void Release(T instance)
        {
            if (instance == null)
            {
                return;
            }

            instance.OnDespawn();
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_parent, worldPositionStays: false);
            _inactiveInstances.Push(instance);
        }

        private T CreateInstance()
        {
            return Object.Instantiate(_prefab, _parent);
        }
    }
}
