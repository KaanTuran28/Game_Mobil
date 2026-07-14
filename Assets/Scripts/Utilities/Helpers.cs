using UnityEngine;

namespace GameMobil.Utilities
{
    /// <summary>Birden fazla sistemde tekrar eden, sınıfa özgü olmayan yardımcı metotlar.</summary>
    public static class Helpers
    {
        /// <summary>
        /// Unity'nin "sahte null" (destroy edilmiş ama referansı hâlâ tutulan) nesnelerini de
        /// güvenle yakalayan null kontrolü. <c>obj == null</c> yerine kullanılması önerilir.
        /// </summary>
        public static bool IsNullOrDestroyed(Object obj) => obj == null;
    }
}
