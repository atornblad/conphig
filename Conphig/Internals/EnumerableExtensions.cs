using System;
using System.Linq;
using System.Collections.Generic;

namespace ATornblad.Conphig.Internals
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int, IEnumerable<T>, int> action)
        {
            int count = collection.Count();
            int index = 0;
            foreach (var item in collection)
            {
                action(item, index, collection, count);
                ++index;
            }
        }
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            int count = collection.Count();
            int index = 0;
            foreach (var item in collection)
            {
                action(item);
                ++index;
            }
        }

        public static int IndexOf<T>(this T[] array, T item) where T : IEquatable<T>
        {
            if (array == null)
            {
                return -1;
            }
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexOfAny<T>(this T[] array, IEnumerable<T> values) where T : IEquatable<T>
        {
            return IndexOfAny(array, values, 0);
        }

        public static int IndexOfAny<T>(this T[] array, IEnumerable<T> values, int startAt) where T : IEquatable<T>
        {
            if (array == null)
            {
                return -1;
            }
            for (int i = startAt; i < array.Length; ++i)
            {
                if (values.Contains(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static IEnumerable<TOut> SelectAllowNull<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> transformer)
        {
            if (input == null)
            {
                return new TOut[0];
            }

            return input.Select(transformer);
        }

        public static Array ToArrayOfType(this IEnumerable<object> input, Type elementType)
        {
            var objectArray = input.ToArray();
            var outputArray = Array.CreateInstance(elementType, objectArray.Length);
            for (int i = 0; i < objectArray.Length; ++i)
            {
                outputArray.SetValue(objectArray[i], i);
            }
            return outputArray;
        }
    }
}
