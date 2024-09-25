using UnityEngine;

namespace Framework.Attributes
{
    public class RangeVector2 : PropertyAttribute
    {
        public float MinX { get; protected set; }
        public float MaxX { get; protected set; }
        public float MinY { get; protected set; }
        public float MaxY { get; protected set; }

        public RangeVector2(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public RangeVector2(int minX, int maxX, int minY, int maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        protected RangeVector2()
        {
        }
    }
}
