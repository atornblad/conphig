using System;
using System.Globalization;

namespace ATornblad.Conphig
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultAttribute : Attribute
    {
        private readonly object value;
        private readonly Type type;
        public DefaultAttribute(object value)
        {
            this.value = value;
            type = value.GetType();
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            if (type == this.type || Nullable.GetUnderlyingType(type) == this.type)
            {
                return value;
            }
            else if (this.type == typeof(string))
            {
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    return DateTime.Parse((string)value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }
                else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                {
                    return DateTimeOffset.Parse((string)value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }
                throw new InvalidOperationException($"Wrong type for default value Get({type.Name}) called, but this.type=={this.type.Name})");
            }
            else
            {
                throw new InvalidOperationException($"Wrong type for default value Get({type.Name}) called, but this.type=={this.type.Name})");
            }
        }
    }
}
