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

        public static IEnumerable<int> IndicesOfAny<T>(this T[] array, IEnumerable<T> values) where T : IEquatable<T>
        {
            int next = IndexOfAny(array, values);
            while (next != -1)
            {
                yield return next;
                next = IndexOfAny(array, values, next + 1);
            }
        }

        public static IEnumerable<TOut> SelectAllowNull<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> transformer)
        {
            if (input == null)
            {
                return Array.Empty<TOut>();
            }

            return input.Select(transformer);
        }

        public static Array ToArrayOfType(this IEnumerable<object?> input, Type elementType)
        {
            var objectArray = input.ToArray();
            var outputArray = Array.CreateInstance(elementType, objectArray.Length);
            for (int i = 0; i < objectArray.Length; ++i)
            {
                outputArray.SetValue(objectArray[i], i);
            }
            return outputArray;
        }

        public static Array ExtendWith(this Array array, Type elementType, object? element)
        {
            int currentLength = array.GetLength(0);
            var newArray = Array.CreateInstance(elementType, currentLength + 1);
            for (int i = 0; i < currentLength; ++i)
            {
                newArray.SetValue(array.GetValue(i), i);
            }
            newArray.SetValue(element, currentLength);
            return newArray;
        }
    }
}
