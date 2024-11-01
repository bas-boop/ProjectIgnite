using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework.Gameplay.MiniGames.Objects;
using Player;

namespace Framework.Gameplay.MiniGames
{
    public sealed class MiniGameSystem : MonoBehaviour
    {
        private const string ERROR_NO_POW = "You need to reference a MovingPaw class!";
        
        [Header("References")]
        [SerializeField] private MiniGame currentMiniGame;
        [field: SerializeField] public InputParser PlayerInput { get; private set; }
        [SerializeField] private GameObject panel;

        [Header("Paw references")]
        [SerializeField] private MovingPaw paw;
        [SerializeField] private GameObject candy;
        [SerializeField] private GameObject weapon;
        [SerializeField] private Vector2 weaponOffset;

        [Header("Cucumber references")]
        [SerializeField] SidewaysMovingCat cucumber;


        [Space, Header("Settings")]
        [SerializeField] private MiniGameType type;
        [SerializeField] private Rect miniGameBounds;

        [Header("Scaling")]
        [SerializeField] private float speed = 1;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private AnimationCurve animationCurve;

        [Header("Events"), Space]
        [SerializeField] private UnityEvent onGrow = new();
        [SerializeField] private UnityEvent onShrink = new();
        
        private Vector2 _size;
        private bool _isSwapped;

        private void Awake()
        {
            _size = transform.localScale;
            panel.transform.localScale = miniGameBounds.size;

            if (type == MiniGameType.SWAP_WEAPON
                && paw == null)
                throw new Exception(ERROR_NO_POW);
        }

        private void Start() => transform.localScale = Vector3.zero;

        public Vector2 MiniGameBounds() => miniGameBounds.size * 0.5f;
        
        public void Activate()
        {
            StartCoroutine(Scaling(true));
            
            if (type == MiniGameType.SWAP_WEAPON)
                PlayerInput.SetCurrentPaw(paw);
            else if(type == MiniGameType.CUCUMBER)
                PlayerInput.SetCurrentCucumber(cucumber);
        }

        public void Deactivate()
        {
            StartCoroutine(Scaling(false));
            
            if (type == MiniGameType.SWAP_WEAPON)
                PlayerInput.SetCurrentPaw(null);
            if (type == MiniGameType.CUCUMBER)
                PlayerInput.SetCurrentCucumber(null);
        }

        public void SwapWeaponWitchCandy()
        {
            if (_isSwapped)
                return;

            _isSwapped = true;
            
            Transform candyParent = candy.transform.parent;
            Transform weaponParent = weapon.transform.parent;

            candy.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);
            weapon.transform.SetLocalPositionAndRotation((Vector3) weaponOffset + candy.transform.position, 
                                                            candy.transform.rotation);
            
            candy.transform.SetParent(weaponParent);
            weapon.transform.SetParent(candyParent);
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