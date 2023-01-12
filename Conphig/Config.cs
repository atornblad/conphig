using System;
using System.Linq;
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

            Json.Apply(config, filename);
            Environment.Apply(config, EnvironmentVariableGetter);
            CommandLine.Apply(config, CommandLineArgsGetter == null ? null : CommandLineArgsGetter());

            return config;
        }
    }
}
