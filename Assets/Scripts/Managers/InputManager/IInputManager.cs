using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>Unity Input System üzerinden okunan girdiyi soyutlayan manager'ın sözleşmesi.</summary>
    public interface IInputManager : IInitializable
    {
        /// <summary>Sürekli okunan hareket ekseni (poll edilir, event değildir - her frame event yayınlamak GC/perf açısından maliyetlidir).</summary>
        Vector2 MoveInput { get; }

        /// <summary>Sürekli okunan bakış/kamera ekseni.</summary>
        Vector2 LookInput { get; }
    }
}
