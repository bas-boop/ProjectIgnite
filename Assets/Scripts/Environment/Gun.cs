using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;

namespace Environment
{
    public sealed class Gun : MonoBehaviour
    {
        [SerializeField, Tag] private string playerTag;
        [SerializeField, RangeVector2(-360, 360, -3600, 360)] private Vector2 angle = new (-45, 45);
        [SerializeField] private GameObject e;
        [SerializeField] private List<Rigidbody2D> childSquares = new ();

        [SerializeField] private UnityEvent onPlayerEnter;
        [SerializeField] private UnityEvent onPlayerExit;

        private bool _isDestroyed;
        private bool _canInteract;
        
        public float shootForce = 10f;

        private void Update()
        {
            if (_isDestroyed
                || !_canInteract
                || !Input.GetKeyDown(KeyCode.E))
                return;

            BeDestroyed();
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

        private void BeDestroyed()
        {
            foreach (Rigidbody2D rb in childSquares)
            {
                float randomAngle = Random.Range(angle.x, angle.y);
                Vector2 direction = new (Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
                
                rb.gravityScale = 1;
                rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
            }

            _isDestroyed = true;
            Destroy(e);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}