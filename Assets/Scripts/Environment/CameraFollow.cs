using System;
using UnityEngine;

using Framework.Extensions;

namespace Environment
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;

        private Camera _this;
        private float _cameraDistance;
        private float _cameraHeight;

        private void Awake()
        {
            _this = GetComponent<Camera>();
            _cameraDistance = transform.position.z;
        }

        private void Update()
        {
            Vector3 newPos = followTarget.position;
            newPos.SetY(_cameraHeight);
            newPos.SetZ(_cameraDistance);
            transform.position = newPos;
        }

        public void ZoomIn()
        {
            _this.orthographicSize = 2;
            _cameraHeight = -1.5f;
        }
        
        public void ZoomOut()
        {
            _this.orthographicSize = 5;
            _cameraHeight = 0;
        }
    }
}