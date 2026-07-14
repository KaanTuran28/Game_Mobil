using UnityEngine;

namespace GameMobil.Utilities
{
    /// <summary>
    /// Proje Layer ayarlarındaki (Project Settings > Tags and Layers) katmanlara karşılık gelen
    /// sabitler. Bu oturumda Player/Enemy/Ground/Interactable katmanları projeye eklenmiştir.
    /// </summary>
    public static class LayerConstants
    {
        public const string Default = "Default";
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string Ground = "Ground";
        public const string Interactable = "Interactable";

        public static int PlayerMask => LayerMask.GetMask(Player);
        public static int EnemyMask => LayerMask.GetMask(Enemy);
        public static int GroundMask => LayerMask.GetMask(Ground);
        public static int InteractableMask => LayerMask.GetMask(Interactable);
    }
}
