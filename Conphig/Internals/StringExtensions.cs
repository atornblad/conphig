using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ATornblad.Conphig.Internals
{
    public static class StringExtensions
    {
        public static string ToKebab(this string value)
        {
            return Regex.Replace(value, "(((?<!^)[A-Z](?=[a-z]))|((?<=[a-z])[A-Z]))", "-$1").ToLower();
        }

        public static string AsFilename(this string input)
        {
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                input = input.Replace($"{invalid}", string.Empty);
            }
            foreach (char invalid in Path.GetInvalidPathChars())
            {
                input = input.Replace($"{invalid}", string.Empty);
            }
            return input;
        }

        public static string AsFilename(this string input, string extension)
        {
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                input = input.Replace($"{invalid}", string.Empty);
            }
            foreach (char invalid in Path.GetInvalidPathChars())
            {
                input = input.Replace($"{invalid}", string.Empty);
            }
            return Path.ChangeExtension(input, extension);
        }
    }
}
