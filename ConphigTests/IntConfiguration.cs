using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class IntConfiguration
    {
        [CommandLine("--nintjsonenvcl")]
        [EnvironmentVariable("NINT_JSON_ENV_CL")]
        [JsonPropertyName("nIntJsonEnvCl")]
        public int? NIntJsonEnvCl { get; set; }

        [CommandLine("--nintjsonenvcl1")]
        [EnvironmentVariable("NINT_JSON_ENV_CL_1")]
        [JsonPropertyName("nIntJsonEnvCl1")]
        public int? NIntJsonEnvCl1 { get; set; } = 1;

        [CommandLine("--nlongjsonenvcl2")]
        [EnvironmentVariable("NLONG_JSON_ENV_CL_2")]
        [JsonPropertyName("nLongJsonEnvCl2")]
        public long? NLongJsonEnvCl2 { get; set; } = 2L;

        [CommandLine("--intjsonenvcl")]
        [EnvironmentVariable("INT_JSON_ENV_CL")]
        [JsonPropertyName("intJsonEnvCl")]
        public int IntJsonEnvCl { get; set; }

        [CommandLine("--longjsonenvcl")]
        [EnvironmentVariable("LONG_JSON_ENV_CL")]
        [JsonPropertyName("longJsonEnvCl")]
        public long LongJsonEnvCl { get; set; } = 3L;
    }
}
