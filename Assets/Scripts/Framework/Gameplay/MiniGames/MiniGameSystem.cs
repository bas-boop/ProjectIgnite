using System;
using System.Collections;
using UnityEngine;

using Player;
using UnityEngine.Events;

namespace Framework.Gameplay.MiniGames
{
    public sealed class MiniGameSystem : MonoBehaviour
    {
        private const string ERROR_NO_POW = "You need to reference a MovingPaw class!";
        
        [Header("References")]
        [SerializeField] private MiniGame currentMiniGame;
        [SerializeField] private MovingPaw paw;
        [field: SerializeField] public InputParser PlayerInput { get; private set; }
        [SerializeField] private GameObject panel;

        [Space, Header("Settings")]
        [SerializeField] private Rect miniGameBounds;
        [SerializeField] private MinigameType type;

        [Header("Scaling")]
        [SerializeField] private float speed = 1;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private AnimationCurve animationCurve;

        [Header("Events"), Space]
        [SerializeField] private UnityEvent onGrow = new();
        [SerializeField] private UnityEvent onShrink = new();
        
        private Vector2 _size;

        private void Awake()
        {
            _size = transform.localScale;
            panel.transform.localScale = miniGameBounds.size;

            if (type == MinigameType.SWAP_WEAPON
                && paw == null)
                throw new Exception(ERROR_NO_POW);
        }

        private void Start() => transform.localScale = Vector3.zero;

        public Vector2 MiniGameBounds() => miniGameBounds.size * 0.5f;
        
        public void Activate()
        {
            StartCoroutine(Scaling(true));
            
            if (type == MinigameType.SWAP_WEAPON)
                PlayerInput.SetCurrentPaw(paw);
        }

        public void Deactivate()
        {
            StartCoroutine(Scaling(false));
            
            if (type == MinigameType.SWAP_WEAPON)
                PlayerInput.SetCurrentPaw(null);
        }

        private IEnumerator Scaling(bool shouldGrow)
        {
            Vector2 targetSize = shouldGrow ? _size : Vector2.zero;
            Vector2 initialSize = transform.localScale;
            float time = 0f;

            while (time < duration)
            {
                time += speed * Time.deltaTime;
                float timeFraction = Mathf.Clamp01(time / duration);
                float curveValue = animationCurve.Evaluate(timeFraction);
                transform.localScale = Vector3.Lerp(initialSize, targetSize, curveValue);
        
                yield return null;
            }
            
            transform.localScale = targetSize;
            UnityEvent onDoneScaling = shouldGrow ? onGrow : onShrink;
            onDoneScaling.Invoke();
        }
    }
}