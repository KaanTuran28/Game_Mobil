using System.Collections;
using GameMobil.Core;
using GameMobil.Data;
using GameMobil.Events;
using GameMobil.Interfaces;
using GameMobil.ScriptableObjects;
using UnityEngine;

namespace GameMobil.Managers
{
    /// <summary>
    /// Müzik ve SFX çalma, master ses seviyesi ve mute yönetiminden sorumlu manager.
    /// Gameplay/UI kodu bu sınıfı doğrudan çağırmaz; <see cref="EventBus"/> üzerinden
    /// yayınlanan <see cref="PlayMusicRequestEvent"/>, <see cref="PlaySfxRequestEvent"/>,
    /// <see cref="SetMasterVolumeEvent"/> ve <see cref="SetMuteEvent"/> event'lerine tepki verir.
    /// </summary>
    /// <remarks>
    /// Inspector'da atanacak <see cref="AudioConfig"/> ve <see cref="AudioSource"/> referansları
    /// gerektirdiği için MonoBehaviour'dur. SFX kaynakları GC/CPU maliyetini azaltmak için
    /// <see cref="IPoolManager"/> üzerinden havuzlanır (mobilde Instantiate/Destroy yerine).
    /// </remarks>
    public class AudioManager : MonoBehaviour, IAudioManager, ISaveable
    {
        [SerializeField] private AudioConfig _audioConfig;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private PooledAudioSource _sfxSourcePrefab;

        private IPoolManager _poolManager;
        private float _masterVolume = 1f;
        private bool _isMuted;

        public float MasterVolume => _masterVolume;
        public bool IsMuted => _isMuted;
        public string SaveId => "AudioManager";

        public void Initialize()
        {
            _poolManager = GameServices.Get<IPoolManager>();

            EventBus.Subscribe<PlayMusicRequestEvent>(OnPlayMusicRequested);
            EventBus.Subscribe<PlaySfxRequestEvent>(OnPlaySfxRequested);
            EventBus.Subscribe<SetMasterVolumeEvent>(OnSetMasterVolumeRequested);
            EventBus.Subscribe<SetMuteEvent>(OnSetMuteRequested);
        }

        public void Shutdown()
        {
            EventBus.Unsubscribe<PlayMusicRequestEvent>(OnPlayMusicRequested);
            EventBus.Unsubscribe<PlaySfxRequestEvent>(OnPlaySfxRequested);
            EventBus.Unsubscribe<SetMasterVolumeEvent>(OnSetMasterVolumeRequested);
            EventBus.Unsubscribe<SetMuteEvent>(OnSetMuteRequested);
        }

        public void PlayMusic(string trackId)
        {
            if (_musicSource == null || _audioConfig == null)
            {
                Debug.LogWarning("AudioManager: MusicSource veya AudioConfig atanmamış.");
                return;
            }

            var clip = _audioConfig.GetMusicClip(trackId);
            if (clip == null)
            {
                Debug.LogWarning($"AudioManager: '{trackId}' id'li müzik klibi bulunamadı.");
                return;
            }

            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource?.Stop();
        }

        public void PlaySfx(string clipId)
        {
            if (_audioConfig == null || _sfxSourcePrefab == null)
            {
                Debug.LogWarning("AudioManager: AudioConfig veya SfxSourcePrefab atanmamış.");
                return;
            }

            var clip = _audioConfig.GetSfxClip(clipId);
            if (clip == null)
            {
                Debug.LogWarning($"AudioManager: '{clipId}' id'li SFX klibi bulunamadı.");
                return;
            }

            var pooledSource = _poolManager.Get(_sfxSourcePrefab);
            pooledSource.Source.volume = _isMuted ? 0f : _masterVolume;
            pooledSource.Source.clip = clip;
            pooledSource.Source.Play();
            StartCoroutine(ReleaseAfterPlayback(pooledSource, clip.length));
        }

        public void SetMasterVolume(float volume)
        {
            _masterVolume = Mathf.Clamp01(volume);
            ApplyVolume();
        }

        public void SetMute(bool isMuted)
        {
            _isMuted = isMuted;
            ApplyVolume();
        }

        public object CaptureState() => new AudioSaveData(_masterVolume, _isMuted);

        public void RestoreState(object state)
        {
            if (state is AudioSaveData data)
            {
                _masterVolume = data.MasterVolume;
                _isMuted = data.IsMuted;
                ApplyVolume();
            }
        }

        private void ApplyVolume()
        {
            if (_musicSource != null)
            {
                _musicSource.volume = _isMuted ? 0f : _masterVolume;
            }
        }

        private IEnumerator ReleaseAfterPlayback(PooledAudioSource pooledSource, float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            _poolManager.Release(pooledSource);
        }

        private void OnPlayMusicRequested(PlayMusicRequestEvent evt) => PlayMusic(evt.TrackId);
        private void OnPlaySfxRequested(PlaySfxRequestEvent evt) => PlaySfx(evt.ClipId);
        private void OnSetMasterVolumeRequested(SetMasterVolumeEvent evt) => SetMasterVolume(evt.Volume);
        private void OnSetMuteRequested(SetMuteEvent evt) => SetMute(evt.IsMuted);
    }
}
