using UnityEngine;

namespace GameMobil.Extensions
{
    /// <summary>Transform üzerinde sık kullanılan işlemler için extension method'lar.</summary>
    public static class TransformExtensions
    {
        /// <summary>Transform'un dünya pozisyonunun X bileşenini değiştirir.</summary>
        public static void SetX(this Transform transform, float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        /// <summary>Transform'un dünya pozisyonunun Y bileşenini değiştirir.</summary>
        public static void SetY(this Transform transform, float y)
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
        }

        /// <summary>Transform'un tüm alt nesnelerini yok eder.</summary>
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
