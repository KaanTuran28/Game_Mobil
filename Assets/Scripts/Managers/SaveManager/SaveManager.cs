using System.Collections.Generic;
using GameMobil.Core;
using GameMobil.Events;
using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>
    /// Kayıt edilebilir sistemlerin merkezi kayıt defteri. Somut sistemleri (AudioManager,
    /// GameManager vb.) tanımaz; sadece <see cref="ISaveable"/> sözleşmesi üzerinden çalışır
    /// (Dependency Inversion) — yeni bir sistem kaydedilmek istediğinde bu sınıf değişmez.
    /// </summary>
    /// <remarks>
    /// ÖNEMLİ: Bu, sadece mimari iskelettir. <see cref="RequestSave"/> ve <see cref="RequestLoad"/>
    /// henüz hiçbir dosya/JSON/PlayerPrefs kalıcılığı yapmaz; yalnızca akışın tamamlandığını
    /// duyuran event'leri yayınlar. Gerçek serileştirme mantığı kasıtlı olarak yazılmamıştır.
    /// </remarks>
    public class SaveManager : ISaveManager
    {
        private readonly List<ISaveable> _saveables = new List<ISaveable>();

        public void Initialize()
        {
        }

        public void Shutdown()
        {
            _saveables.Clear();
        }

        public void Register(ISaveable saveable)
        {
            if (saveable != null && !_saveables.Contains(saveable))
            {
                _saveables.Add(saveable);
            }
        }

        public void Unregister(ISaveable saveable)
        {
            _saveables.Remove(saveable);
        }

        public void RequestSave()
        {
            // TODO: Gerçek kalıcılık (ör. Application.persistentDataPath altına JSON yazımı)
            // ileride bu metodun içinde implemente edilecek. Mobilde OnApplicationPause
            // sırasında da tetiklenmesi gerektiği unutulmamalı (Android arka planı bildirimsiz kapatabilir).
            EventBus.Publish(new SaveCompletedEvent());
        }

        public void RequestLoad()
        {
            // TODO: Gerçek kalıcılık (ör. dosyadan okuma + RestoreState çağrıları)
            // ileride bu metodun içinde implemente edilecek.
            EventBus.Publish(new LoadCompletedEvent());
        }
    }
}
