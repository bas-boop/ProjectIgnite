using Framework.Extensions;
using UnityEngine;

namespace Framework.Gameplay.MiniGames
{
    public class DraggableObject : MonoBehaviour
    {
        [SerializeField] private MiniGameSystem miniGameSystem;
        
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
        }
    }
}