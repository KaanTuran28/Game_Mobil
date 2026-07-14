using GameMobil.Interfaces;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// PoolManager üzerinden havuzlanabilen bir AudioSource sarmalayıcısı. Her SFX çalma isteğinde
    /// yeni bir AudioSource Instantiate/Destroy etmek yerine bu bileşen havuzdan alınıp bırakılır.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PooledAudioSource : MonoBehaviour, IPoolable
    {
        public AudioSource Source { get; private set; }

        private void Awake()
        {
            Source = GetComponent<AudioSource>();
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            Source.Stop();
            Source.clip = null;
        }
    }
}
