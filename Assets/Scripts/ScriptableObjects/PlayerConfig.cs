using UnityEngine;

namespace GameMobil.ScriptableObjects
{
    /// <summary>
    /// Oyuncuya ait temel sayısal değerlerin saf veri tanımı. Bu sınıf herhangi bir davranış
    /// (hareket, hasar alma vb.) içermez; ileride yazılacak Player script'leri bu veriyi tüketecektir.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Config/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _moveSpeed = 5f;

        public float MaxHealth => _maxHealth;
        public float MoveSpeed => _moveSpeed;
    }
}
