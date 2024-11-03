using UnityEngine;

namespace NPC
{
    public sealed class Bobbing : MonoBehaviour
    {
        [SerializeField] private Vector2 speed = Vector2.one;
        [SerializeField] private float height = 0.5f;
        
        private Vector3 _startPos;
        private float _speed;

        private void Start()
        {
            _startPos = transform.position;
            _speed = Random.Range(speed.x, speed.y);
        }

        private void Update()
        {
            float newY = _startPos.y + Mathf.Sin(Time.time * _speed) * height;
            transform.position = new (transform.position.x, newY, _startPos.z);
        }
    }
}