using UnityEngine;

namespace Boids
{
    [System.Serializable]
    public class Target
    {
        // Type of target.
        public enum Type
        {
            Attractor,
            Repeller
        }
        // Shape of target.
        public enum Shape
        {
            Sphere,
            OutsideSphere,
            Box,
            OutsideBox,
        }

        public Type type;
        public Shape shape;
        public Vector3 position;
        public Vector3 scale;
        public float strength;

        // Gradient function returns a vector where it's direction is the direction of the greates decrease in the distance to the targeted shape.
        public Vector3 Gradient(Vector3 point)
        {
            Vector3 p = position - point;
            Vector3 s = scale * 0.5f;
            switch (shape)
            {
                case Shape.Sphere:
                    if (p.sqrMagnitude >= Mathf.Pow(Mathf.Max(Mathf.Max(scale.x, scale.y), scale.z), 2)) break;
                    return p.normalized;

                case Shape.OutsideSphere:
                    if (p.sqrMagnitude < Mathf.Pow(Mathf.Max(Mathf.Max(scale.x, scale.y), scale.z), 2)) break;
                    return -p.normalized;

                case Shape.Box:
                    if (p.x >= s.x || p.x <= -s.x || p.y >= s.y || p.y <= -s.y || p.z >= s.z || p.z <= -s.z) break;
                    if (p.x >= p.y && p.x >= p.z) return new Vector3(Mathf.Sign(p.x), 0, 0);
                    if (p.y >= p.z) return new Vector3(0, Mathf.Sign(p.y), 0);
                    return new Vector3(0, 0, Mathf.Sign(p.z));

                case Shape.OutsideBox:
                    return -new Vector3(Mathf.Clamp((int)(p.x / (scale.x * 0.5f)), -1, 1), Mathf.Clamp((int)(p.y / (scale.y * 0.5f)), -1, 1), Mathf.Clamp((int)(p.z / (scale.z * 0.5f)), -1, 1)).normalized;
            };

            return Vector3.zero;
        }
        public void DebugDraw()
        {
            switch (shape)
            {
                case Shape.OutsideSphere:
                case Shape.Sphere: Gizmos.DrawWireSphere(position, Mathf.Max(Mathf.Max(scale.x, scale.y), scale.z)); return;
                case Shape.OutsideBox:
                case Shape.Box: Gizmos.DrawWireCube(position, scale); return;
            };
        }
    }
}