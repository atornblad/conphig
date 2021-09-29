using System;
using System.Text.Json.Serialization;

namespace ConphigTests
{
    public class ArrayConfiguration
    {
        [JsonPropertyName("strings")]
        public string[] Strings { get; set; }

        [JsonPropertyName("ints")]
        public int[] Ints { get; set; }

        [JsonPropertyName("longs")]
        public long[] Longs { get; set; }

        [JsonPropertyName("bools")]
        public bool[] Bools { get; set; }

        [JsonPropertyName("floats")]
        public float[] Floats { get; set; }

        [JsonPropertyName("doubles")]
        public double[] Doubles { get; set; }

        [JsonPropertyName("datetimes")]
        public DateTime[] DateTimes { get; set; }

        [JsonPropertyName("datetimeoffsets")]
        public DateTimeOffset[] DateTimeOffsets { get; set; }
    }
}
