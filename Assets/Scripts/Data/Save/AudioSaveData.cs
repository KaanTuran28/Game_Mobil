using System;

namespace GameMobil.Data
{
    /// <summary>
    /// AudioManager'ın <see cref="GameMobil.Interfaces.ISaveable"/> üzerinden yakaladığı durumun veri modeli.
    /// Saf veri taşıyıcısıdır; herhangi bir dosya/JSON kalıcılık mantığı içermez.
    /// </summary>
    [Serializable]
    public struct AudioSaveData
    {
        public float MasterVolume;
        public bool IsMuted;

        public AudioSaveData(float masterVolume, bool isMuted)
        {
            MasterVolume = masterVolume;
            IsMuted = isMuted;
        }
    }
}
