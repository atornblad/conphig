using System;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConphigTests")]

namespace ATornblad.Conphig.Internals
{
    internal static class Conversion
    {
        public static object ChangeType(object input, Type targetType)
        {
            var nullable = Nullable.GetUnderlyingType(targetType);
            if (nullable != null)
            {
                if (object.ReferenceEquals(input, null))
                {
                    return Activator.CreateInstance(targetType);
                }
                var innerValue = ChangeType(input, nullable);
                return Activator.CreateInstance(targetType, new object[] { innerValue });
            }
            if (input == null)
            {
                if (targetType.IsClass)
                {
                    return input;
                }
                else
                {
                    throw new InvalidCastException($"Cannot convert a null value to {targetType.FullName}");
                }
            }

            if (targetType.IsInstanceOfType(input))
            {
                return input;
            }

            if (input is string str)
            {
                if (str.Length == 0 && targetType.IsClass)
                {
                    return null;
                }
                else if (targetType == typeof(DateTime))
                {
                    return DateTime.Parse(str, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }
                else if (targetType == typeof(DateTimeOffset))
                {
                    return DateTimeOffset.Parse(str, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }
                else if (targetType == typeof(bool))
                {
                    return str.ToLower() == "true";
                }
                else if (targetType == typeof(int))
                {
                    return int.Parse(str, NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
                else if (targetType == typeof(long))
                {
                    return long.Parse(str, NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
                else if (targetType == typeof(float))
                {
                    return float.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
                }
                else if (targetType == typeof(double))
                {
                    return double.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
                }
                else
                {
                    try
                    {
                        return Convert.ChangeType(str, targetType);
                    }
                    catch
                    {
                        throw new InvalidCastException($"Cannot convert a string value to {targetType.FullName}");
                    }
                }
            }

            try
            {
                return Convert.ChangeType(input, targetType);
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert a {input.GetType().FullName} value to {targetType.FullName}");
            }
        }
    }
}
