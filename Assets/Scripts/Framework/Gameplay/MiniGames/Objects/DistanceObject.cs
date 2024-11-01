using UnityEngine;
using UnityEngine.Events;

using Framework.Extensions;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class DistanceObject : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float checkRange = 2;
        [SerializeField] private UnityEvent onReached = new();
        
        private bool _shouldCheck;
        
        private void Update()
        {
            if (_shouldCheck
                && transform.position.IsWithinRange(targetTransform.position, checkRange))
                onReached?.Invoke();
        }

        public void SetActive(bool target) => _shouldCheck = target;
    }
}