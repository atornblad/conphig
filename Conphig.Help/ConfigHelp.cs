using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Conphig.Help.Tests")]

namespace ATornblad.Conphig
{
    public static class ConfigHelp
    {
        public static void WriteToConsole<TConfig>() where TConfig : class, new()
        {
            WriteTo<TConfig>(System.Console.Out, System.Environment.GetCommandLineArgs()[0]);
        }
        public static void WriteToConsole<TConfig>(string programName) where TConfig : class, new()
        {
            WriteTo<TConfig>(System.Console.Out, programName);
        }

        public static void WriteTo<TConfig>(TextWriter output, string programName) where TConfig : class, new()
        {
            var args = System.Environment.GetCommandLineArgs();
            output.Write($"USAGE: {programName}");

            var propsWithCommandLine = from prop in typeof(TConfig).GetProperties()
                                       let attr = prop.GetCustomAttribute<CommandLineAttribute>()
                                       where attr != null
                                       select new { prop, attr };

            var singleLetterBoolPropertiesWithCommandLine = from pa in propsWithCommandLine
                                                            where pa.prop.PropertyType == typeof(bool) || pa.prop.PropertyType == typeof(bool?)
                                                            let shortSwitches = pa.attr.SwitchNames.Where(s => s.Length == 2 && s.StartsWith("-")).Select(s => s.Substring(1)).ToArray()
                                                            where shortSwitches.Any()
                                                            select new { pa.prop, shortSwitches };
            
            if (singleLetterBoolPropertiesWithCommandLine.Any())
            {
                output.Write(" [-");
                output.Write(string.Join("", singleLetterBoolPropertiesWithCommandLine.SelectMany(s => s.shortSwitches).OrderBy(s => s, StringComparer.InvariantCulture)));
                output.Write("]");
            }

            var otherPropertiesWithCommandLine = from pa in propsWithCommandLine
                                                 where singleLetterBoolPropertiesWithCommandLine.All(s => s.prop != pa.prop)
                                                 select pa;
            
            if (otherPropertiesWithCommandLine.Any())
            {
                output.Write(" [");
                var valuePropTexts = from p in otherPropertiesWithCommandLine
                                     let shortName = p.attr.SwitchNames.FirstOrDefault(n => Regex.IsMatch(n, "^-[^-]"))
                                     let anyName = p.attr.SwitchNames.First()
                                     let nameUsed = shortName ?? anyName
                                     let sortOrder = nameUsed.TrimStart('-').ToLower()
                                     let valuePrompt = GetValuePrompt(p.prop.PropertyType)
                                     orderby sortOrder
                                     select $"{nameUsed} {valuePrompt}".Trim();
                output.Write(string.Join(" ", valuePropTexts));
                output.Write("]");
            }

            output.WriteLine();
        }

        private static string GetValuePrompt(Type propertyType)
        {
            var useType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            
            if (useType == typeof(bool))
                return string.Empty;
            else if (useType == typeof(string))
                return "TEXT";
            else if (useType == typeof(int))
                return "NUMBER";
            else
                return "VALUE";
        }
    }
}