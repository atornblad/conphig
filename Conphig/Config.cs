using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;
using ATornblad.Conphig.Internals;

namespace ATornblad.Conphig
{
    public static class Config
    {
        public static Func<string[]> CommandLineArgsGetter { get; set; } = null;
        public static Func<string, string> EnvironmentVariableGetter { get; set; } = null;

        public static T Load<T>() where T : class, new()
        {
            var filenameBasedOnType = typeof(T).Name.ToKebab().AsFilename(".json");
            var filename = typeof(T).GetCustomAttributes(typeof(FilenameAttribute), false).OfType<FilenameAttribute>()
                .Select(fa => fa.Filename)
                .FirstOrDefault()
                ?? filenameBasedOnType;

            return Load<T>(filename);
        }

        public static T Load<T>(string filename) where T : class, new()
        {
            T config = new();

            Defaults.Apply(config);
            Json.Apply(config, filename);
            Environment.Apply(config, EnvironmentVariableGetter);
            CommandLine.Apply(config, CommandLineArgsGetter == null ? null : CommandLineArgsGetter());

            return config;
        }
    }
}
