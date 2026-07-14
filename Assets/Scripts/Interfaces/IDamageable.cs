namespace GameMobil.Interfaces
{
    /// <summary>
    /// Hasar alabilen ve ölüm/yok olma durumu bulunan her nesnenin (Player, Enemy, yıkılabilir objeler)
    /// uyacağı ortak sözleşme. Somut gameplay davranışı (can azaltma efekti, ölüm animasyonu vb.)
    /// bu altyapı aşamasında implemente edilmemiştir.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>Nesnenin güncel canı.</summary>
        float CurrentHealth { get; }

        /// <summary>Nesnenin azami canı.</summary>
        float MaxHealth { get; }

        /// <summary>Nesne hasar aldığında çağrılır.</summary>
        /// <param name="amount">Uygulanacak hasar miktarı.</param>
        void TakeDamage(float amount);

        /// <summary>Nesnenin canı sıfırın altına düşüp düşmediğini belirtir.</summary>
        bool IsDead { get; }
    }
}
