using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

using Framework.Attributes;

namespace Environment
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField, Tag] private string playerTag;
        [SerializeField, Range(1, 100)] private float shootForce = 10f;
        [SerializeField, RangeVector2(-360, 360, -3600, 360)] private Vector2 angle = new (-45, 45);
        [SerializeField] private GameObject interactVisual;
        [SerializeField] private List<Rigidbody2D> childSquares = new ();

        public bool IsDestroyed { get; private set; }
        
        [SerializeField] private UnityEvent onPlayerEnter = new();
        [SerializeField] private UnityEvent onPlayerExit = new();
        [SerializeField] private UnityEvent onDestroy = new();

        private Collider2D _collider;
        
        private bool _canInteract;

        private void Awake() => _collider = GetComponent<Collider2D>();

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

        public void Interact()
        {
            if (IsDestroyed
                || !_canInteract)
                return;

            BeDestroyed();
        }
        
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