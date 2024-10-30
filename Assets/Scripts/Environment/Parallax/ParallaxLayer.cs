using System;
using UnityEngine;

namespace Environment.Parallax
{
    [Serializable]
    public struct ParallaxLayer
    {
        public Transform layer;
        public float speed;
    }
}