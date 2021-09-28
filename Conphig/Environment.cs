using System;
using System.Linq;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    internal static class Environment
    {
        internal static void Apply<T>(T target, Func<string, string> envVarGetter = null)
        {
            var getter = envVarGetter ?? System.Environment.GetEnvironmentVariable;

            typeof(T)
                .GetProperties()
                .Select(p => new {
                    PropertyInfo = p,
                    EVarAttribute = (EnvironmentVariableAttribute)p.GetCustomAttributes(typeof(EnvironmentVariableAttribute), false).FirstOrDefault()
                })
                .Where(peva => peva.EVarAttribute != null)
                .Select(peva => new {
                    peva.PropertyInfo,
                    peva.EVarAttribute.VariableName
                })
                .ForEach((pivn) => {
                    object currentValue = pivn.PropertyInfo.GetValue(target);
                    string envValue = getter(pivn.VariableName);
                    if (envValue != null)
                    {
                        var type = Nullable.GetUnderlyingType(pivn.PropertyInfo.PropertyType) ?? pivn.PropertyInfo.PropertyType;
                        object typedValue = Conversion.ChangeType(envValue, type);
                        pivn.PropertyInfo.SetValue(target, typedValue);
                    }
                });
        }
    }
}
