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
                    foreach (var switchIndex in switchIndices)
                    {
                        HandleProperty(target, args, pin.PropertyInfo, switchIndex);
                    }
                });
        }

        private static void HandleProperty<T>(T target, string[] args, PropertyInfo propertyInfo, int switchIndex)
        {
            var type = propertyInfo.PropertyType;
            if (switchIndex != -1)
            {
                var underlyingNullableType = Nullable.GetUnderlyingType(type);
                if (type == typeof(bool) || underlyingNullableType == typeof(bool))
                {
                    propertyInfo.SetValue(target, true);
                }
                else if (switchIndex < args.Length - 1)
                {
                    if (type.IsArray)
                    {
                        var elementType = type.GetElementType()!;
                        var element = Conversion.ChangeType(args[switchIndex + 1], elementType);

                        object? existingValue = propertyInfo.GetValue(target);
                        if (existingValue == null)
                        {
                            // Make new array with one element
                            var array = Array.CreateInstance(elementType, 1);
                            array.SetValue(element, 0);
                            propertyInfo.SetValue(target, array);
                        }
                        else
                        {
                            // Make a new array with one MORE element
                            var newArray = ((Array)existingValue).ExtendWith(elementType, element);
                            propertyInfo.SetValue(target, newArray);
                        }
                    }
                    else
                    {
                        propertyInfo.SetValue(target, Conversion.ChangeType(args[switchIndex + 1], type));
                    }
                }
            }
        }
    }
}
