using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;

namespace Framework.Gameplay.MiniGames
{
    public class DraggableObject : MonoBehaviour
    {
        [SerializeField] private MiniGameSystem miniGameSystem;
        [SerializeField] private float detectionSize = 1;
        [SerializeField, Tag] private string placementTag;
        
        private bool _canBeDragged;
        private bool _isBeingDragged;

        private void Update()
        {
            if (!_isBeingDragged)
                return;

            Vector2 mousePos = miniGameSystem.PlayerInput.GetMousePos();
            Vector2 bounds = miniGameSystem.MiniGameBounds();

            mousePos.SetX(Mathf.Clamp(mousePos.x, -bounds.x, bounds.x));
            mousePos.SetY(Mathf.Clamp(mousePos.y, -bounds.y, bounds.y));
            
            transform.position = mousePos;
        }

        private void OnMouseEnter() => _canBeDragged = true;
        
        private void OnMouseExit() => _canBeDragged = false;

        private void OnMouseDown()
        {
            if (_canBeDragged)
                _isBeingDragged = !_isBeingDragged;
            
            if (!_isBeingDragged)
                PlaceOnHolder();
        }

        private void PlaceOnHolder()
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, detectionSize);

            foreach (Collider2D obj in objects)
            {
                if (!obj.CompareTag(placementTag))
                    continue;

                DragObjectPlacement placement = obj.GetComponent<DragObjectPlacement>();
                placement.SetCurrentItem(this);
                break;
            }
        }
    }
}