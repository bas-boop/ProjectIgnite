using System.Collections;
using UnityEngine;

using Player;

namespace Framework.Gameplay.MiniGames
{
    public sealed class MiniGameSystem : MonoBehaviour
    {
        [SerializeField] private MiniGame currentMiniGame;
        [field: SerializeField] public InputParser PlayerInput { get; private set; }
        [SerializeField] private GameObject panel;

        [Space, SerializeField] private Rect miniGameBounds;

        [Header("Scaling")]
        [SerializeField] private float speed = 1;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private AnimationCurve animationCurve;
        
        private Vector2 _size;

        private void Awake()
        {
            _size = transform.localScale;
            panel.transform.localScale = miniGameBounds.size;
        }

        private void Start() => transform.localScale = Vector3.zero;

        public Vector2 MiniGameBounds() => miniGameBounds.size * 0.5f;
        
        public void Activate() => StartCoroutine(Scaling(true));
        
        public void Deactivate() => StartCoroutine(Scaling(false));

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
        }
    }
}