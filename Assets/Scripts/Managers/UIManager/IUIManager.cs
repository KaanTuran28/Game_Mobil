using GameMobil.Interfaces;

namespace GameMobil.Managers
{
    /// <summary>Ekran/panel (screen/panel) gösterim yığınını yöneten manager'ın sözleşmesi.</summary>
    public interface IUIManager : IInitializable
    {
        /// <summary>Bir ekran prefab'ını id ile kayıt eder (henüz sahnede oluşturulmaz).</summary>
        void RegisterScreen(string screenId, UnityEngine.GameObject screenPrefab);

        /// <summary>Kayıtlı bir ekranı gösterir (gerekirse ilk kullanımda Instantiate eder).</summary>
        void ShowScreen(string screenId);

        /// <summary>Gösterilen bir ekranı gizler.</summary>
        void HideScreen(string screenId);

        /// <summary>Tüm açık ekranları gizler.</summary>
        void HideAll();
    }
}
