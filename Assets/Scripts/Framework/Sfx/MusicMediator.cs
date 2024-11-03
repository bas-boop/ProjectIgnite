using UnityEngine;

namespace Framework.Sfx
{
    public sealed class MusicMediator : MonoBehaviour
    {
        [SerializeField] private bool stopOnWake;

        private void Awake()
        {
            if (stopOnWake)
                DoStop();
        }

        public void DoStart() => MusicPlayer.Instance.StartMusic();

        public void DoStop() => MusicPlayer.Instance.StopMusic();
    }
}