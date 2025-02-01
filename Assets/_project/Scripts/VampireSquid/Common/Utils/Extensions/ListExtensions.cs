using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Random = UnityEngine.Random;

namespace VampireSquid.Common.Utils.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var temp   = list[i];
                var random = Random.Range(0, list.Count);

                list[i]      = list[random];
                list[random] = temp;
            }
        }

        public static T PickRandomWithForce<T>(this List<T> objects, Expression<Func<T, float>> selector)
        {
            var func = selector.Compile();

            if (objects == null || !objects.Any()) throw new NullReferenceException("list is null or empty");

            var greatPool = objects.Sum(i => func(i));

            if (objects.Count == 1) return objects[0];
            if (greatPool == 0) return objects[0];

            var point = Random.Range(0, greatPool);

            var pickPool = 0f;
            foreach (var i in objects)
            {
                pickPool += func(i);
                if (pickPool > point) return i;
            }

            throw new NullReferenceException($"Cannot pick object, pool: {greatPool}, elements: {objects.Count}, picked Random: {pickPool}");
        }

        public static T PickRandom<T>(this List<T> list)
        {
            var listCount = list.Count;
            if (listCount == 0) return default;
            var randomIndex = UnityEngine.Random.Range(0, listCount);
            return list[randomIndex];
        }

        public static T PickRandom<T>(this IEnumerable<T> enumerable)
        {
            var listCount = enumerable.Count();
            if (listCount == 0) return default;
            var randomIndex = UnityEngine.Random.Range(0, listCount);
            return enumerable.ElementAt(randomIndex);
        }

        public static T PickRandom<T>(this T[] array)
        {
            var listCount = array.Length;
            if (listCount == 0) return default;
            var randomIndex = UnityEngine.Random.Range(0, listCount);
            return array[randomIndex];
        }
    }
}
