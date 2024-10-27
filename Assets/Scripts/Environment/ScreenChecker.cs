using System;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public sealed class ScreenChecker : MonoBehaviour
    {
        [SerializeField] private List<TargetScreenInfo> screenPositions; 
        
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
                Debug.LogError("Main camera not found.");
        }

        private void Update()
        {
            CheckPositions();

            foreach (TargetScreenInfo screenInfo in screenPositions) 
                Debug.Log(screenInfo.position + " " + screenInfo.target.name);
        }

        private void CheckPositions()
        {
            for (int i = 0; i < screenPositions.Count; i++)
            {
                TargetScreenInfo targetScreenInfo = screenPositions[i];
                Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetScreenInfo.target.position);

                ScreenPosition position = screenPoint.x switch
                {
                    >= 0 when screenPoint.x <= Screen.width 
                              && screenPoint.y >= 0 
                              && screenPoint.y <= Screen.height =>
                        ScreenPosition.INSIDE,
                    < 0 => ScreenPosition.OUTSIDE_LEFT,
                    _ => screenPoint.x > Screen.width
                        ? ScreenPosition.OUTSIDE_RIGHT
                        : ScreenPosition.INSIDE
                };

                targetScreenInfo.position = position;
                screenPositions[i] = targetScreenInfo;
            }
        }
    }
}
