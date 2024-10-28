using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UI.World;

namespace Environment
{
    public sealed class ScreenChecker : MonoBehaviour
    {
        [SerializeField] private float margin = 0.5f;
        [SerializeField] private GameObject leftUI;
        [SerializeField] private GameObject rightUI;
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
            UpdateUI();
        }

        private void CheckPositions()
        {
            float leftBound = 0 + margin;
            float rightBound = Screen.width - margin;

            for (int i = 0; i < screenPositions.Count; i++)
            {
                TargetScreenInfo targetScreenInfo = screenPositions[i];
                Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetScreenInfo.target.position);

                ScreenPosition position;

                if (screenPoint.x >= leftBound
                    && screenPoint.x <= rightBound
                    && screenPoint.y >= 0
                    && screenPoint.y <= Screen.height)
                    position = ScreenPosition.INSIDE;
                else if (screenPoint.x < leftBound)
                    position = ScreenPosition.OUTSIDE_LEFT;
                else if (screenPoint.x > rightBound)
                    position = ScreenPosition.OUTSIDE_RIGHT;
                else
                    position = ScreenPosition.INSIDE;

                targetScreenInfo.position = position;
                screenPositions[i] = targetScreenInfo;
            }
        }

        private void UpdateUI()
        {
            bool l = false;
            bool r = false;
            
            foreach (TargetScreenInfo screenInfo in screenPositions)
            {
                switch (screenInfo.position)
                {
                    case ScreenPosition.INSIDE:
                        break;
                    case ScreenPosition.OUTSIDE_LEFT:
                        l = true;
                        break;
                    case ScreenPosition.OUTSIDE_RIGHT:
                        r = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            leftUI.SetActive(l);
            rightUI.SetActive(r);

            ApplyProgress(l, leftUI, ScreenPosition.OUTSIDE_LEFT);
            ApplyProgress(r, rightUI, ScreenPosition.OUTSIDE_RIGHT);
        }

        private void ApplyProgress(bool side,
            GameObject ui,
            ScreenPosition yes)
        {
            if (!side)
                return;
            
            float highestProgress = (from screenInfo in screenPositions where screenInfo.position == yes 
                select screenInfo.target.GetComponent<WeaponTimerUI>().Progress).Prepend(0).Max();

            ui.GetComponent<WeaponTimerUI>().Progress = highestProgress;
        }
    }
}
