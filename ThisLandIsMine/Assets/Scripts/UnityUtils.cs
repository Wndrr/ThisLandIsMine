using UnityEngine;

namespace DefaultNamespace
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 source)
        {
            return new Vector2(source.x, source.y);
        }
    }
}