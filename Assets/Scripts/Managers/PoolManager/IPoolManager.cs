using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>Prefab bazlı generic nesne havuzlarını yöneten manager'ın sözleşmesi.</summary>
    public interface IPoolManager : IInitializable
    {
        /// <summary>Belirtilen prefab için havuzdan bir örnek alır (havuz yoksa oluşturur).</summary>
        T Get<T>(T prefab) where T : Component, IPoolable;

        /// <summary>Bir örneği ait olduğu havuza geri bırakır.</summary>
        void Release<T>(T instance) where T : Component, IPoolable;
    }
}
