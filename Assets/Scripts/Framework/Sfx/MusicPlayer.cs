using UnityEngine;

namespace Framework.Sfx
{
    public sealed class MusicPlayer : Singleton<MusicPlayer>
    {
        private AudioSource _a;

        protected override void Awake()
        {
            base.Awake();
            _a = GetComponent<AudioSource>();
        }

        public void StartMusic() => _a.Play();
        
        public void StopMusic() => _a.Stop();
    }
}