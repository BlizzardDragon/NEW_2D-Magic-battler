using System.Collections.Generic;
using UnityEngine;

namespace VampireSquid.Common.Utils.Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsValueInRange(this Vector2 minMax, float value) =>
            value <= minMax.y && value >= minMax.x;

        public static bool IsNotInRange(this Vector2 minMax, float value) =>
            value > minMax.y && value < minMax.x;

        public static float RangedRandom(this Vector2 range) =>
            Random.Range(range.x, range.y);

        public static float TakeBetween(this Vector2 range, float percent) =>
            Mathf.Lerp(range.x, range.y, percent);

        public static float TakeBetweenUnclamped(this Vector2 range, float percent) =>
            Mathf.LerpUnclamped(range.x, range.y, percent);

        public static Vector3 InvertHorizontal(this Vector3 vector)
        {
            var x = vector.x;
            vector.x = vector.z;
            vector.z = x;
            return vector;
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            var ab = b - a;
            var av = value - a;
            return Vector3.Dot(av, ab) / Vector3.Dot(ab, ab);
        }

        public static float GetAngleFloat(this Vector3 direction)
        {
            direction = direction.normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0) 
                angle += 360;

            return angle;
        }

        public static Vector3 GetPointOutsideOtherRadiuses(this Vector3 point, List<Vector3> otherVectors, float findRadius, float objectRadius)
        {
            Vector3 offsetPoint             = GetRandomOffset(point, findRadius);
            bool    isAllRadiusesConsistent = true;
            int     count                   = 0;

            do
            {
                count++;

                isAllRadiusesConsistent = true;

                for (int i = 0; i < otherVectors.Count; i++)
                {
                    if (Vector3.Distance(otherVectors[i], offsetPoint) <= objectRadius)
                    {
                        isAllRadiusesConsistent = false;
                        offsetPoint             = GetRandomOffset(point, findRadius);
                        break;
                    }
                }
            } while (!isAllRadiusesConsistent);

            return offsetPoint;
        }

        private static Vector3 GetRandomOffset(Vector3 point, float radius) =>
            point + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * radius;

        public static Vector3 RemoveY(this Vector3 v3)
        {
            v3.y = 0f;
            return v3;
        }
    }
}
