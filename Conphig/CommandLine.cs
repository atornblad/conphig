using System;
using System.Diagnostics;
using System.Linq;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    internal static class CommandLine
    {
        public static void Apply<T>(T target, string[] commandLineArgs = null)
        {
            var args = commandLineArgs ?? System.Environment.GetCommandLineArgs();

            typeof(T).GetProperties()
                .Select(p => new {
                    PropertyInfo = p,
                    JPNAttribute = (CommandLineAttribute)p.GetCustomAttributes(typeof(CommandLineAttribute), false).FirstOrDefault()
                })
                .Where(pjpna => pjpna.JPNAttribute != null)
                .Select(pjpna => new {
                    pjpna.PropertyInfo,
                    pjpna.JPNAttribute.SwitchNames
                })
                .ForEach((pin) => {
                    var type = pin.PropertyInfo.PropertyType;
                    int switchIndex = args.IndexOfAny(pin.SwitchNames);
                    if (switchIndex != -1)
                    {
                        var underlyingNullableType = Nullable.GetUnderlyingType(type);
                        if (type == typeof(bool) || underlyingNullableType == typeof(bool))
                        {
                            pin.PropertyInfo.SetValue(target, true);
                        }
                        else
                        {
                            if (type.IsArray)
                            {
                                var elementType = type.GetElementType();

                                while (switchIndex != -1)
                                {
                                    object element = Conversion.ChangeType(args[switchIndex + 1], elementType);

                                    var existingValue = pin.PropertyInfo.GetValue(target);
                                    if (existingValue is null)
                                    {
                                        // Make new array with one element
                                        var array = Array.CreateInstance(elementType, 1);
                                        array.SetValue(element, 0);
                                        pin.PropertyInfo.SetValue(target, array);
                                    }
                                    else
                                    {
                                        // Make a new array with one MORE element
                                        var existingArray = (Array)existingValue;
                                        int currentLength = existingArray.GetLength(0);
                                        var array = Array.CreateInstance(elementType, currentLength + 1);
                                        for (int i = 0; i < currentLength; ++i)
                                        {
                                            array.SetValue(existingArray.GetValue(i), i);
                                        }
                                        array.SetValue(element, currentLength);
                                        pin.PropertyInfo.SetValue(target, array);
                                    }

                                    switchIndex = args.IndexOfAny(pin.SwitchNames, switchIndex + 1);
                                }
                            }
                            else
                            {
                                pin.PropertyInfo.SetValue(target, Conversion.ChangeType(args[switchIndex + 1], type));
                            }
                        }
                    }
                });
        }
    }
}
