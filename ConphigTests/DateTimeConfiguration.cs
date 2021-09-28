using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class DateTimeConfiguration
    {
        [JsonPropertyName("ndtDefNull")]
        [CommandLine("--ndtdefnull")]
        [EnvironmentVariable("NDT_DEF_NULL")]
        public DateTime? NDTDefNull { get; set; }

        [JsonPropertyName("ndtoDefNull")]
        [CommandLine("--ndtodefnull")]
        [EnvironmentVariable("NDTO_DEF_NULL")]
        public DateTimeOffset? NDTODefNull { get; set; }

        [Default("2020-02-03T04:05:06Z")]
        [JsonPropertyName("ndtDefValue")]
        [CommandLine("--ndtdefvalue")]
        [EnvironmentVariable("NDT_DEF_VALUE")]
        public DateTime? NDTDefValue { get; set; }

        [Default("2020-02-03T04:05:06Z")]
        [JsonPropertyName("ndtoDefValue")]
        [CommandLine("--ndtodefvalue")]
        [EnvironmentVariable("NDTO_DEF_VALUE")]
        public DateTimeOffset? NDTODefValue { get; set; }

        [Default("2021-02-03T04:05:06Z")]
        [JsonPropertyName("dtDefValue")]
        [CommandLine("--dtdefvalue")]
        [EnvironmentVariable("DT_DEF_VALUE")]
        public DateTime DTDefValue { get; set; }

        [Default("2021-02-03T04:05:06Z")]
        [JsonPropertyName("dtoDefValue")]
        [CommandLine("--dtodefvalue")]
        [EnvironmentVariable("DTO_DEF_VALUE")]
        public DateTimeOffset DTODefValue { get; set; }
    }
}
