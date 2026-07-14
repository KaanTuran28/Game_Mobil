using UnityEngine;

namespace GameMobil.ScriptableObjects
{
    /// <summary>
    /// Oyun genelindeki temel, gameplay'e özgü olmayan yapılandırma değerlerini tutan ScriptableObject.
    /// İhtiyaç duyulan yeni alanlar buraya eklenerek genişletilebilir.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string _gameVersion = "0.1.0";
        [SerializeField] private int _targetFrameRate = 60;

        public string GameVersion => _gameVersion;
        public int TargetFrameRate => _targetFrameRate;
    }
}
