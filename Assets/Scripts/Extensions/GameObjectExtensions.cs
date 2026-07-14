using UnityEngine;

namespace GameMobil.Extensions
{
    /// <summary>GameObject üzerinde sık kullanılan işlemler için extension method'lar.</summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Belirtilen bileşeni döndürür; yoksa ekleyip döndürür. Tekrarlanan
        /// <c>GetComponent</c>/<c>AddComponent</c> çağrı ikilisini tek satıra indirger.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent<T>(out var component) ? component : gameObject.AddComponent<T>();
        }
    }
}
