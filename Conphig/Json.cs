using System;
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
                            JPNAttribute = (JsonPropertyNameAttribute?)p.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false).FirstOrDefault()
                        })
                        .Where(pjpna => pjpna.JPNAttribute != null)
                        .Select(pjpna => new
                        {
                            pjpna.PropertyInfo,
                            pjpna.JPNAttribute!.Name
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
            object? valueToSet = GetValue(propertyInfo.PropertyType, elementName, value);
            propertyInfo.SetValue(config, valueToSet);
        }

        private static object? GetValue(Type outputType, string elementName, JsonElement value)
        {
            if (outputType.IsArray)
            {
                var elementType = outputType.GetElementType()!;

                return Enumerable.Range(0, value.GetArrayLength())
                    .Select(i => GetValue(elementType, $"{elementName}[{i}]", value[i]))
                    .ToArrayOfType(elementType);
            }

            var nullable = Nullable.GetUnderlyingType(outputType);
            if (nullable != null)
            {
                var inner = GetValue(nullable, elementName, value);
                if (inner == null)
                {
                    return null;
                }
                else
                {
                    return Activator.CreateInstance(outputType, new object[] { inner });
                }
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            else if (value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
            {
                return HandleBoolean(value, outputType, elementName);
            }
            else if (value.ValueKind == JsonValueKind.String)
            {
                return HandleString(value, outputType);
            }
            else if (value.ValueKind == JsonValueKind.Number)
            {
                return HandleNumber(value, outputType, elementName);
            }
            else
            {
                throw new NotImplementedException($"Converting JSON property {elementName} from {value.ValueKind}");
            }
        }

        private static object HandleBoolean(JsonElement value, Type outputType, string elementName)
        {
            if (outputType == typeof(bool))
            {
                return value.GetBoolean();
            }
            else
            {
                throw new InvalidOperationException($"Converting JSON property {elementName} from boolean to {outputType.FullName}");
            }
        }

        private static object? HandleString(JsonElement value, Type outputType)
        {
            if (outputType == typeof(string))
            {
                return value.GetString();
            }
            else
            {
                return Conversion.ChangeType(value.GetString(), outputType);
            }
        }

        private static object? HandleNumber(JsonElement value, Type outputType, string elementName)
        {
            if (outputType == typeof(int))
            {
                return value.GetInt32();
            }
            else if (outputType == typeof(long))
            {
                return value.GetInt64();
            }
            else if (outputType == typeof(float))
            {
                return value.GetSingle();
            }
            else if (outputType == typeof(double))
            {
                return value.GetDouble();
            }
            else
            {
                try
                {
                    return Conversion.ChangeType(value.GetString(), outputType);
                }
                catch
                {
                    throw new InvalidOperationException($"Converting JSON property {elementName} from string to {outputType.FullName}");
                }
            }
        }
    }
}
