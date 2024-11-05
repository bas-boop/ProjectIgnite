using System.Collections.Generic;
using UnityEngine;

using Framework.Extensions;

namespace Environment.Parallax
{
    public sealed class ParallaxEffect : MonoBehaviour
    {
        [Header("Always needed")]
        [SerializeField] private float accelerationTime = 2f;
        [Header("Settings for camera")]
        [SerializeField] private bool followCamera;
        [SerializeField, Tooltip("Use this for automatic scroll when not following camera")]
        private float scrollSpeed;
        
        [Space(20)]
        [SerializeField] private ParallaxLayer[] layers;

        private readonly Dictionary<Transform, float> _parallaxScales = new ();
        private Transform _cam;
        private Vector2 _followPoint;
        private Vector2 _previousFollowPoint;
        private bool _canScroll = true;
        private bool _isAccelerating = true;
        private float _elapsedTime;
        private float _currentScrollSpeed;

        private void Awake()
        {
            foreach (ParallaxLayer layer in layers)
            {
                _parallaxScales.Add(layer.layer, layer.speed);
            }

            if (followCamera)
                _cam = Camera.main.transform;
        }

        private void Update()
        {
            if (!followCamera)
                UpdateFollowPoint();

            if (!_canScroll)
                return;
            
            UpdateElapsedTime();
            ScrollBackgrounds();
        }
        
        public void InvertScroll()
        {
            scrollSpeed = -scrollSpeed;
            SetScrolling(true);
        }

        public void SetScrolling(bool target)
        {
            _canScroll = target;
            _elapsedTime = 0f;
            _isAccelerating = true;
        }

        private void ScrollBackgrounds()
        {
            Vector2 currentFollowPoint = followCamera ? _cam.position : _followPoint;

            foreach (Transform layer in _parallaxScales.Keys)
            {
                float parallaxScale = _parallaxScales[layer];
                Vector3 currentPosition = layer.position;

                Vector3 backgroundTargetPos = new (
                    currentPosition.x + (_previousFollowPoint.x - currentFollowPoint.x) * parallaxScale,
                    currentPosition.y + (_previousFollowPoint.y - currentFollowPoint.y) * parallaxScale,
                    currentPosition.z);

                layer.position = Vector3.Lerp(currentPosition, backgroundTargetPos, Time.deltaTime * _currentScrollSpeed);
            }

            _previousFollowPoint = currentFollowPoint;
        }

        private void UpdateFollowPoint() => _previousFollowPoint.SubtractX(scrollSpeed);
        
        private void UpdateElapsedTime()
        {
            if (!_isAccelerating)
                return;
            
            _elapsedTime += Time.deltaTime;
            float targetScrollSpeed = Mathf.Abs(scrollSpeed);

            if (_elapsedTime >= accelerationTime)
            {
                _currentScrollSpeed = targetScrollSpeed;
                _isAccelerating = false;
            }
            else
            {
                float time = _elapsedTime / accelerationTime;
                _currentScrollSpeed = Mathf.Lerp(_currentScrollSpeed, targetScrollSpeed, time);
            }
        }
    }
}