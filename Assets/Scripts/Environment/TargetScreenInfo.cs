using System;
using UnityEngine;

namespace Environment
{
    [Serializable]
    public struct TargetScreenInfo
    {
        public Transform target;
        public ScreenPosition position;

        public TargetScreenInfo(Transform target, ScreenPosition position)
        {
            this.target = target;
            this.position = position;
        }
    }
}