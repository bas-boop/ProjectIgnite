using UnityEngine;

namespace UI.Canvas
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField, Tooltip("Maximum rotation angle in degrees.")] private float rotationMagnitude = 15f;

        private void Update() => AnimateTitle();

        private void AnimateTitle()
        {
            float angle = Mathf.Sin(Time.time * animationSpeed) * rotationMagnitude;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}