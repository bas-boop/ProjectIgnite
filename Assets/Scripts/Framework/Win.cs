using UnityEngine;
using UnityEngine.Events;

using NPC;

namespace Framework
{
    public sealed class Win : MonoBehaviour
    {
        [SerializeField] private Emotion[] emotions;
        [SerializeField] private UnityEvent onEveryBodyHappy;

        private bool _isInvoked;
        
        private void Update()
        {
            if (_isInvoked)
                return;
            
            foreach (Emotion e in emotions)
            {
                if (!e.IsHappy)
                    return;
                
                onEveryBodyHappy?.Invoke();
                _isInvoked = true;
            }
        }
    }
}