namespace GameMobil.Interfaces
{
    /// <summary>
    /// Bootstrapper tarafından sıralı şekilde başlatılan/kapatılan tüm manager'ların uyduğu sözleşme.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>Manager'ı kullanıma hazırlar. Bootstrapper tarafından bağımlılık sırasına göre çağrılır.</summary>
        void Initialize();

        /// <summary>Manager'ın abone olduğu event'leri ve tuttuğu referansları serbest bırakır.</summary>
        void Shutdown();
    }
}
