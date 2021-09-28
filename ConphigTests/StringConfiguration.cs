using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class StringConfiguration
    {
        [CommandLine("--first")]
        [EnvironmentVariable("STRING_FIRST")]
        [JsonPropertyName("first")]
        public string FirstNoDefault { get; set; }

        [Default("x")]
        [CommandLine("--second")]
        [EnvironmentVariable("STRING_SECOND")]
        [JsonPropertyName("second")]
        public string SecondDefaultX { get; set; }

        [Default("y")]
        [CommandLine("--third")]
        [EnvironmentVariable("STRING_THIRD")]
        [JsonPropertyName("third")]
        public string ThirdDefaultY { get; set; }
    }
}
