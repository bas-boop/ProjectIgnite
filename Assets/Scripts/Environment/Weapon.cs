using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

using Framework;
using Framework.Attributes;
using Framework.Gameplay;
using Framework.Gameplay.MiniGames;
using Player;

namespace Environment
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private MiniGameSystem miniGameSystem;
        [SerializeField, Tag] private string playerTag;
        [SerializeField, Range(1, 100)] private float shootForce = 10f;
        [SerializeField, RangeVector2(-360, 360, -3600, 360)] private Vector2 angle = new (-45, 45);
        [SerializeField] private GameObject interactVisual;
        [SerializeField] private List<Rigidbody2D> childSquares = new ();

        public GameObject WeaponUI { get; set; }
        public Timer WeaponTimer { get; set; }
        public bool IsDestroyed { get; private set; }
        public bool IsActive { get; private set; }
        
        [SerializeField] private UnityEvent onInteract = new();
        [SerializeField] private UnityEvent onPlayerEnter = new();
        [SerializeField] private UnityEvent onPlayerExit = new();
        [SerializeField] private UnityEvent onDestroy = new();

        private Collider2D _collider;
        private WeaponManager _parent;
        private Interacter _interacter;

        private bool _isCreated;
        private bool _canInteract;

        private void Awake() => _collider = GetComponent<Collider2D>();

        private void OnEnable()
        {
            if (!_isCreated)
            {
                OnCreate();
                return;
            }
            
            WeaponUI.gameObject.SetActive(true);
            WeaponTimer.SetTimerLength(30);
            Invoke(nameof(StartTimer), 0.001f);
        }

        private void Start()
        {
            Interacter interacter = FindObjectOfType<Interacter>();
            interacter.AddWeapon(this);

            CameraFollow cf = FindObjectOfType<CameraFollow>();
            onPlayerEnter.AddListener(cf.ZoomIn);
            onPlayerExit.AddListener(cf.ZoomOut);
            
            onDestroy.AddListener(_parent.CheckByEachWeapon);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(playerTag)) 
                return;
            
            _canInteract = true;
            onPlayerEnter?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(playerTag))
                return;
            
            _canInteract = false;
            onPlayerExit?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(playerTag)) 
                _canInteract = true;
        }

        public void SpawnWeapon()
        {
            transform.parent.gameObject.SetActive(true);
            IsActive = true;
        }
        
        public bool Interact()
        {
            if (IsDestroyed
                || !_canInteract)
                return false;
            
            onInteract?.Invoke();
            return true;
        }

        public void Des()
        {
            if (IsDestroyed)
                return;
            
            IsDestroyed = true;
            onDestroy?.Invoke();
            _interacter.RemoveWeapon(this);
            Destroy(WeaponUI.gameObject);
            Destroy(interactVisual);
        }

        private void StartTimer() => WeaponTimer.SetCanCount(true);

        private void OnCreate()
        {
            _interacter = FindObjectOfType<Interacter>();
            _interacter.AddWeapon(this);
            
            _parent = FindObjectOfType<WeaponManager>();
            _parent.AddWeapon(this);

            _isCreated = true;
        }
        
        /// <summary>
        /// Old function
        /// </summary>
        private void BeDestroyed()
        {
            foreach (Rigidbody2D rb in childSquares)
            {
                float randomAngle = Random.Range(angle.x, angle.y);
                Vector2 direction = new (Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
                
                rb.gravityScale = 1;
                rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
            }

            _collider.enabled = false;
            IsDestroyed = true;
            onDestroy?.Invoke();
            Destroy(interactVisual);
        }
    }
}