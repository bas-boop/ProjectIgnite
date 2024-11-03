using System;
using UnityEngine;

using Framework.Extensions;

namespace Environment
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private bool vertical = true;
        [SerializeField] private float zoomHeight = -0.5f;

        private Camera _this;
        private float _cameraDistance;
        private float _cameraHeight;
        private float _startCameraHeight;

        private void Awake()
        {
            _this = TryGetComponent(out Camera c) 
                ? c 
                : Camera.main;
            
            _cameraDistance = transform.position.z;
            _startCameraHeight = transform.position.y;
        }

        private void Start()
        {
            if (followTarget == null)
                followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            Vector3 newPos = followTarget.position;
            newPos.SetY(vertical ? _cameraHeight : 0);
            newPos.SetZ(_cameraDistance);
            transform.position = newPos;
        }

        public void ZoomIn()
        {
            _this.orthographicSize = 4;
            _cameraHeight = zoomHeight;
        }
        
        public void ZoomOut()
        {
            _this.orthographicSize = 5;
            _cameraHeight = _startCameraHeight;
        }
    }
}