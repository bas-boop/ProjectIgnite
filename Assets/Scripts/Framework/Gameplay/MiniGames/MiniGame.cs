using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.MiniGames
{
    public sealed class MiniGame : MonoBehaviour
    {
        [SerializeField] private UnityEvent onComplete = new();

        public void Complete() => onComplete?.Invoke();
    }
}