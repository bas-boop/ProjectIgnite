using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.MiniGames
{
    public sealed class DragObjectPlacement : MonoBehaviour
    {
        [field: SerializeField] public UnityEvent OnHold { get; private set; }

        private DraggableObject _draggableObject;

        public void SetCurrentItem(DraggableObject target)
        {
            _draggableObject = target;
            _draggableObject.transform.position = transform.position;
            OnHold?.Invoke();
        }
    }
}