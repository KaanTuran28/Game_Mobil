namespace GameMobil.Interfaces
{
    /// <summary>
    /// PoolManager tarafından yönetilen, havuzdan alınıp bırakılabilen nesnelerin sözleşmesi.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>Nesne havuzdan alınıp aktif edildiğinde çağrılır (state sıfırlama için).</summary>
        void OnSpawn();

        /// <summary>Nesne havuza geri bırakılmadan hemen önce çağrılır (temizlik için).</summary>
        void OnDespawn();
    }
}
