using System;
using System.Collections.Generic;
using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Core
{
    /// <summary>
    /// Tip-güvenli, global publish/subscribe event bus'ı. Manager'lar ve gameplay sistemleri
    /// birbirine doğrudan referans vermek yerine bu sınıf üzerinden haberleşir.
    /// Global ve durumsuz (tek bir bus yeterli) olduğu için kasıtlı olarak static tutulmuştur;
    /// ayrı bir arayüz/ServiceLocator kaydı gerektirmez.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> Subscribers = new Dictionary<Type, Delegate>();

        /// <summary>Belirtilen event tipine abone olur.</summary>
        public static void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
        {
            if (handler == null)
            {
                return;
            }

            var eventType = typeof(TEvent);
            if (Subscribers.TryGetValue(eventType, out var existing))
            {
                Subscribers[eventType] = Delegate.Combine(existing, handler);
            }
            else
            {
                Subscribers[eventType] = handler;
            }
        }

        /// <summary>Belirtilen event tipinden aboneliği kaldırır.</summary>
        public static void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
        {
            if (handler == null)
            {
                return;
            }

            var eventType = typeof(TEvent);
            if (!Subscribers.TryGetValue(eventType, out var existing))
            {
                return;
            }

            var remaining = Delegate.Remove(existing, handler);
            if (remaining == null)
            {
                Subscribers.Remove(eventType);
            }
            else
            {
                Subscribers[eventType] = remaining;
            }
        }

        /// <summary>Bir event'i yayınlar; abone olan tüm handler'lar senkron olarak çağrılır.</summary>
        public static void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
        {
            var eventType = typeof(TEvent);
            if (!Subscribers.TryGetValue(eventType, out var existing))
            {
                return;
            }

            (existing as Action<TEvent>)?.Invoke(gameEvent);
        }

        /// <summary>
        /// Unity Editor'de "Enter Play Mode Options" ile domain reload kapatıldığında static alanların
        /// eski oturumdan kalan abonelikleri taşımaması için, her play moduna girişte bus temizlenir.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayModeEnter()
        {
            Subscribers.Clear();
        }
    }
}
