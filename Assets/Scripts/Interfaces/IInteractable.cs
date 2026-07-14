namespace GameMobil.Interfaces
{
    /// <summary>
    /// Oyuncunun etkileşime girebileceği nesnelerin (kapı, sandık, NPC vb.) uyacağı ortak sözleşme.
    /// Somut etkileşim mantığı bu altyapı aşamasında implemente edilmemiştir.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>Nesnenin şu anda etkileşime açık olup olmadığı.</summary>
        bool CanInteract { get; }

        /// <summary>Etkileşim tetiklendiğinde çağrılır.</summary>
        /// <param name="instigator">Etkileşimi başlatan nesne (genellikle oyuncu).</param>
        void Interact(object instigator);
    }
}
