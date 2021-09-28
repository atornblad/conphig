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
                    int switchIndex = args.IndexOfAny(pin.SwitchNames);
                    if (switchIndex != -1)
                    {
                        var underlyingNullableType = Nullable.GetUnderlyingType(pin.PropertyInfo.PropertyType);
                        if (pin.PropertyInfo.PropertyType == typeof(bool) || underlyingNullableType == typeof(bool))
                        {
                            pin.PropertyInfo.SetValue(target, true);
                        }
                        else
                        {
                            if (pin.PropertyInfo.PropertyType == typeof(string))
                            {
                                pin.PropertyInfo.SetValue(target, args[switchIndex + 1]);
                            }
                            else
                            {
                                pin.PropertyInfo.SetValue(target, Conversion.ChangeType(args[switchIndex + 1], pin.PropertyInfo.PropertyType));
                            }
                        }
                    }
                });
        }
    }
}
