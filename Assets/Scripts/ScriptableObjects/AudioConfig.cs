using System;
using UnityEngine;

namespace GameMobil.ScriptableObjects
{
    /// <summary>
    /// Müzik ve SFX kliplerinin id -> AudioClip eşlemesini tutan, tasarımcıların kod değiştirmeden
    /// yeni ses ekleyebilmesini sağlayan ScriptableObject (Open/Closed). Henüz hiçbir ses dosyası
    /// atanmamıştır; bu sadece genişletilebilir veri yapısıdır.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Game/Config/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        [Serializable]
        public struct AudioClipEntry
        {
            public string Id;
            public AudioClip Clip;
            [Range(0f, 1f)] public float Volume;
        }

        [SerializeField] private AudioClipEntry[] _musicTracks = Array.Empty<AudioClipEntry>();
        [SerializeField] private AudioClipEntry[] _sfxClips = Array.Empty<AudioClipEntry>();

        /// <summary>Verilen id'ye karşılık gelen müzik klibini döndürür; bulunamazsa null döner.</summary>
        public AudioClip GetMusicClip(string id) => Find(_musicTracks, id);

        /// <summary>Verilen id'ye karşılık gelen SFX klibini döndürür; bulunamazsa null döner.</summary>
        public AudioClip GetSfxClip(string id) => Find(_sfxClips, id);

        private static AudioClip Find(AudioClipEntry[] entries, string id)
        {
            if (string.IsNullOrEmpty(id) || entries == null)
            {
                return null;
            }

            foreach (var entry in entries)
            {
                if (entry.Id == id)
                {
                    return entry.Clip;
                }
            }

            return null;
        }
    }
}
