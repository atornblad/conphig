using System;
using System.Linq;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    internal static class Defaults
    {
        public static void Apply<T>(T target)
        {
            typeof(T)
                .GetProperties()
                .Select(p => new {
                    PropertyInfo = p,
                    DVAttribute = (DefaultAttribute)p.GetCustomAttributes(typeof(DefaultAttribute), false).FirstOrDefault()
                })
                .Where(pida => pida.DVAttribute != null)
                .Select(pida => new {
                    pida.PropertyInfo,
                    DefaultValue = pida.DVAttribute.Get(pida.PropertyInfo.PropertyType)
                })
                .ForEach((pidv) => {
                    pidv.PropertyInfo.SetValue(target, Conversion.ChangeType(pidv.DefaultValue, pidv.PropertyInfo.PropertyType));
                });
        }
    }
}
