using System;
using System.Linq;
using System.Reflection;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    internal static class CommandLine
    {
        public static void Apply<T>(T target, string[]? commandLineArgs = null)
        {
            var args = commandLineArgs ?? System.Environment.GetCommandLineArgs();

            typeof(T).GetProperties()
                .Select(p => new {
                    PropertyInfo = p,
                    JPNAttribute = (CommandLineAttribute?)p.GetCustomAttributes(typeof(CommandLineAttribute), false).FirstOrDefault()
                })
                .Where(pjpna => pjpna.JPNAttribute != null)
                .Select(pjpna => new {
                    pjpna.PropertyInfo,
                    pjpna.JPNAttribute!.SwitchNames
                })
                .ForEach((pin) => {
                    var switchIndices = args.IndicesOfAny(pin.SwitchNames);
                    HandleProperty(target, args, pin.PropertyInfo, switchIndices.ToArray());
                });
        }

        private static void HandleProperty<T>(T target, string[] args, PropertyInfo propertyInfo, int[] switchIndices)
        {
            if (switchIndices.Length == 0)
            {
                return;
            }

            var type = propertyInfo.PropertyType;
            var underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (type == typeof(bool) || underlyingNullableType == typeof(bool))
            {
                propertyInfo.SetValue(target, true);
            }
            else
            {
                var validIndices = switchIndices.Where(i => i < args.Length - 1).ToArray();
                if (validIndices.Length == 0)
                {
                    return;
                }

                if (type.IsArray)
                {
                    var elementType = type.GetElementType()!;
                    var elements = validIndices.Select(i => Conversion.ChangeType(args[i + 1], elementType)).ToArray();

                    // Make new array with the right number of element
                    var array = Array.CreateInstance(elementType, elements.Length);
                    for (int i = 0; i < elements.Length; ++i)
                    {
                        array.SetValue(elements[i], i);
                    }
                    propertyInfo.SetValue(target, array);
                }
                else
                {
                    propertyInfo.SetValue(target, Conversion.ChangeType(args[validIndices.Last() + 1], type));
                }
            }
        }
    }
}
