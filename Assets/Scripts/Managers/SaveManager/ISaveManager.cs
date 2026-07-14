using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>
    /// Kayıtlı <see cref="ISaveable"/> sistemlerini takip eden manager'ın sözleşmesi.
    /// Gerçek dosya/JSON kalıcılık algoritması bu aşamada kapsam dışıdır.
    /// </summary>
    public interface ISaveManager : IInitializable
    {
        /// <summary>Bir sistemi kayıt akışına dahil eder.</summary>
        void Register(ISaveable saveable);

        /// <summary>Bir sistemi kayıt akışından çıkarır.</summary>
        void Unregister(ISaveable saveable);

        /// <summary>Kayıt akışını tetikler (henüz gerçek kalıcılık içermez).</summary>
        void RequestSave();

        /// <summary>Yükleme akışını tetikler (henüz gerçek kalıcılık içermez).</summary>
        void RequestLoad();
    }
}
