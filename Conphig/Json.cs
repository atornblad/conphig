using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    public static class Json
    {
        public static void Apply<T>(T target, string filename) where T : class, new()
        {
            if (File.Exists(filename))
            {
                using (var stream = File.Open(filename, FileMode.Open))
                {
                    var document = JsonDocument.Parse(stream, new JsonDocumentOptions
                    {
                        AllowTrailingCommas = true,
                        CommentHandling = JsonCommentHandling.Skip
                    });
                    var properties = typeof(T)
                        .GetProperties()
                        .Select(p => new
                        {
                            PropertyInfo = p,
                            JPNAttribute = (JsonPropertyNameAttribute)p.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false).FirstOrDefault()
                        })
                        .Where(pjpna => pjpna.JPNAttribute != null)
                        .Select(pjpna => new
                        {
                            pjpna.PropertyInfo,
                            pjpna.JPNAttribute.Name
                        })
                        .ToDictionary(pin => pin.Name, pin => pin.PropertyInfo);
                    foreach (var element in document.RootElement.EnumerateObject())
                    {
                        if (properties.TryGetValue(element.Name, out var propertyInfo))
                        {
                            SetValue(target, propertyInfo, element.Name, element.Value);
                        }
                    }
                }
            }
        }

        private static void SetValue<T>(T config, PropertyInfo propertyInfo, string elementName, JsonElement value) where T : class, new()
        {
            if (value.ValueKind == JsonValueKind.Null)
            {
                if (propertyInfo.PropertyType.IsClass)
                {
                    propertyInfo.SetValue(config, null);
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
                {
                    propertyInfo.SetValue(config, Activator.CreateInstance(propertyInfo.PropertyType));
                }
                else
                {
                    throw new InvalidOperationException($"Converting JSON property {elementName} from null to {propertyInfo.PropertyType.FullName}");
                }
            }
            else if (value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
            {
                if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(config, value.GetBoolean());
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(bool))
                {
                    propertyInfo.SetValue(config, new bool?(value.GetBoolean()));
                }
                else
                {
                    throw new InvalidOperationException($"Converting JSON property {elementName} from boolean to {propertyInfo.PropertyType.FullName}");
                }
            }
            else if (value.ValueKind == JsonValueKind.String)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(config, value.GetString());
                }
                else
                {
                    propertyInfo.SetValue(config, Conversion.ChangeType(value.GetString(), propertyInfo.PropertyType));
                }
            }
            else if (value.ValueKind == JsonValueKind.Number)
            {
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(config, value.GetInt32());
                }
                else if (propertyInfo.PropertyType == typeof(long))
                {
                    propertyInfo.SetValue(config, value.GetInt64());
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(int))
                {
                    propertyInfo.SetValue(config, new int?(value.GetInt32()));
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(long))
                {
                    propertyInfo.SetValue(config, new long?(value.GetInt64()));
                }
                else if (propertyInfo.PropertyType == typeof(float))
                {
                    propertyInfo.SetValue(config, value.GetSingle());
                }
                else if (propertyInfo.PropertyType == typeof(double))
                {
                    propertyInfo.SetValue(config, value.GetDouble());
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(float))
                {
                    propertyInfo.SetValue(config, new float?(value.GetSingle()));
                }
                else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(double))
                {
                    propertyInfo.SetValue(config, new double?(value.GetDouble()));
                }
                else
                {
                    try
                    {
                        propertyInfo.SetValue(config, Conversion.ChangeType(value.GetString(), propertyInfo.PropertyType));
                    }
                    catch
                    {
                        throw new InvalidOperationException($"Converting JSON property {elementName} from string to {propertyInfo.PropertyType.FullName}");
                    }
                }
            }
            else
            {
                throw new NotImplementedException($"Converting JSON property {elementName} from {value.ValueKind}");
            }
        }
    }
}
