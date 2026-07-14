using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameMobil.Core
{
    /// <summary>
    /// Hafif bir servis lokatörü. Manager'lar somut sınıflara değil arayüzlerine (ör. IAudioManager)
    /// bu sınıf üzerinden erişir; dağınık "Manager.Instance" tekilleri yerine tek, öngörülebilir
    /// bir erişim noktası sağlar. Sadece Bootstrapper tarafından doldurulur.
    /// </summary>
    public static class GameServices
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        /// <summary>Bir servisi arayüz tipiyle kaydeder. Aynı tip zaten kayıtlıysa üzerine yazar.</summary>
        public static void Register<TService>(TService service)
        {
            if (service == null)
            {
                return;
            }

            Services[typeof(TService)] = service;
        }

        /// <summary>Kayıtlı bir servisi döndürür. Kayıtlı değilse anlamlı bir hata fırlatır.</summary>
        public static TService Get<TService>()
        {
            if (Services.TryGetValue(typeof(TService), out var service))
            {
                return (TService)service;
            }

            throw new InvalidOperationException(
                $"GameServices: {typeof(TService).Name} kaydedilmemiş. Bootstrapper'ın bu servisi kaydettiğinden emin olun.");
        }

        /// <summary>Bir servisin kayıtlı olup olmadığını güvenli şekilde kontrol eder.</summary>
        public static bool TryGet<TService>(out TService service)
        {
            if (Services.TryGetValue(typeof(TService), out var raw))
            {
                service = (TService)raw;
                return true;
            }

            service = default;
            return false;
        }

        /// <summary>Bir servis kaydını kaldırır.</summary>
        public static void Unregister<TService>()
        {
            Services.Remove(typeof(TService));
        }

        /// <summary>
        /// Unity Editor'de domain reload kapalıyken eski oturumdan kalan servis referanslarının
        /// bir sonraki play moduna sızmaması için tüm kayıtları temizler.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayModeEnter()
        {
            Services.Clear();
        }
    }
}
