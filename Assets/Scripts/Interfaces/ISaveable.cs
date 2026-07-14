namespace GameMobil.Interfaces
{
    /// <summary>
    /// SaveManager'a kendini kaydettirebilen sistemlerin sözleşmesi.
    /// Not: Bu arayüz sadece durum yakalama/geri yükleme sözleşmesini tanımlar;
    /// gerçek dosya/JSON kalıcılık mantığı henüz implemente edilmedi.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>Kayıt dosyası içinde bu sistemi ayırt eden benzersiz kimlik.</summary>
        string SaveId { get; }

        /// <summary>Kaydedilecek mevcut durumu döndürür.</summary>
        object CaptureState();

        /// <summary>Daha önce yakalanmış bir durumu geri yükler.</summary>
        void RestoreState(object state);
    }
}
