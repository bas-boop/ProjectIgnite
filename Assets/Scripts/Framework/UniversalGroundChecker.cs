using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    /// <summary>
    /// Universal ground checker supporting 2D and 3D environments with line and sphere casts.
    /// Supports both offset and transform-based ground checking.
    /// </summary>
    public sealed class UniversalGroundChecker : MonoBehaviour
    {
        #region SerializeField fields
        
        [Header("Usage")]
        [SerializeField, Tooltip("True for 3D settings.\nFalse for 2D settings.")] private bool is3D = true;
        [SerializeField, Tooltip("True for line.\nFalse for sphere.")] private bool lineOrSphere = true;
        [SerializeField, Tooltip("True for offset.\nFalse for transform.")] private bool offSetOrTransform;
        
        [Header("Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float rayCastLength = 1f;
        [SerializeField] private float sphereCastRadius = 1f;
        [SerializeField] private Vector2 offSet2D;
        [SerializeField] private Vector3 offSet3D;
        [SerializeField] private Transform groundCheckerTransform;
        
        [Header("Debug")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool gizmos;
        [SerializeField] private Color gizmosColor = Color.cyan;
        
        #endregion

        #region Private fields

        private enum GroundedState
        {
            GROUNDED,
            AIRED
        }

        private bool _isOnGround = false;
        private bool _isLeavingGround = true;
        
        private GroundedState _currentState;

        private Vector2 _origin2D;
        private Vector3 _origin3D;

        #endregion

        #region Properties

        public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }

        #endregion
        
        #region Events
        
        [Space(20)]
        [SerializeField] private UnityEvent onGroundEnter = new ();
        [SerializeField] private UnityEvent onGroundLeave = new ();
        
        #endregion

        private void FixedUpdate()
        {
            CalculateGroundRayCasting();
            HandleStateTransitions();
        }

        private void CalculateGroundRayCasting()
        {
            if (is3D)
                _origin3D = !offSetOrTransform
                    ? transform.position + offSet3D
                    : groundCheckerTransform != null ? groundCheckerTransform.position : Vector3.zero;
            else
                _origin2D = !offSetOrTransform
                    ? (Vector2)transform.position + offSet2D
                    : groundCheckerTransform != null ? groundCheckerTransform.position : Vector2.zero;
            

            IsGrounded = GetGround();
        }

        private bool GetGround()
        {
            return lineOrSphere 
                ? is3D
                    ? Physics.RaycastAll(_origin3D, Vector3.down, rayCastLength, groundLayer)
                        ?.Any(collider => collider.collider.gameObject != gameObject) ?? false
                    : Physics2D.RaycastAll(_origin2D, Vector2.down, rayCastLength, groundLayer)
                        ?.Any(collider => collider.collider.gameObject != gameObject) ?? false
                : is3D
                    ? Physics.OverlapSphere(_origin3D, sphereCastRadius, groundLayer)
                        ?.Any(collider => collider.gameObject != gameObject) ?? false
                    : Physics2D.OverlapCircleAll(_origin2D, sphereCastRadius, groundLayer)
                        ?.Any(collider => collider.gameObject != gameObject) ?? false;
        }
        
        private void HandleStateTransitions()
        {
            _currentState = IsGrounded ? GroundedState.GROUNDED : GroundedState.AIRED;
            
            switch (_currentState)
            {
                case GroundedState.GROUNDED when !_isOnGround:
                    onGroundEnter?.Invoke();
                    _isOnGround = true;
                    _isLeavingGround = false;
                    break;
                case GroundedState.AIRED when !_isLeavingGround:
                    onGroundLeave?.Invoke();
                    _isLeavingGround = true;
                    _isOnGround = false;
                    break;
            }
        }
        
        private void OnDrawGizmos()
        {
            if (!gizmos)
                return;

            Vector3 origin = is3D ? _origin3D : _origin2D;
            Gizmos.color = gizmosColor;
            
            if (lineOrSphere)
            {
                Vector3 endPosition = origin + Vector3.down * rayCastLength;
                Gizmos.DrawLine(origin, endPosition);
            }
            else
                Gizmos.DrawWireSphere(origin, sphereCastRadius);
        }
    }
}
