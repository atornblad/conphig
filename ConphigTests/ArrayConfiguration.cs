using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class ArrayConfiguration
    {
        [JsonPropertyName("strings")]
        [CommandLine("-s", "--string")]
        public string[]? Strings { get; set; }

        [JsonPropertyName("ints")]
        [CommandLine("-i", "--int")]
        public int[]? Ints { get; set; }

        [JsonPropertyName("longs")]
        public long[]? Longs { get; set; }

        [JsonPropertyName("bools")]
        public bool[]? Bools { get; set; }

        [JsonPropertyName("floats")]
        public float[]? Floats { get; set; }

        [JsonPropertyName("doubles")]
        public double[]? Doubles { get; set; }

        [JsonPropertyName("datetimes")]
        public DateTime[]? DateTimes { get; set; }

        [JsonPropertyName("datetimeoffsets")]
        [CommandLine("-d", "--dto")]
        public DateTimeOffset[]? DateTimeOffsets { get; set; }
    }
}
