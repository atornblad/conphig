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

        [Default(1)]
        [CommandLine("--nintjsonenvcl1")]
        [EnvironmentVariable("NINT_JSON_ENV_CL_1")]
        [JsonPropertyName("nIntJsonEnvCl1")]
        public int? NIntJsonEnvCl1 { get; set; }

        [Default(2L)]
        [CommandLine("--nlongjsonenvcl2")]
        [EnvironmentVariable("NLONG_JSON_ENV_CL_2")]
        [JsonPropertyName("nLongJsonEnvCl2")]
        public long? NLongJsonEnvCl2 { get; set; }

        [CommandLine("--intjsonenvcl")]
        [EnvironmentVariable("INT_JSON_ENV_CL")]
        [JsonPropertyName("intJsonEnvCl")]
        public int IntJsonEnvCl { get; set; }

        [Default(3L)]
        [CommandLine("--longjsonenvcl")]
        [EnvironmentVariable("LONG_JSON_ENV_CL")]
        [JsonPropertyName("longJsonEnvCl")]
        public long LongJsonEnvCl { get; set; }
    }
}
