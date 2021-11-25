using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class BoolConfiguration
    {
        [CommandLine("-a", "--nbooljsonenvcl")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL")]
        [JsonPropertyName("nBoolJsonEnvCl")]
        public bool? NBoolJsonEnvCl { get; set; }

        [CommandLine("-b", "--nbooljsonenvclt")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL_T")]
        [JsonPropertyName("nBoolJsonEnvClT")]
        public bool? NBoolJsonEnvClT { get; set; } = true;

        [CommandLine("-c", "--nbooljsonenvclf")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL_F")]
        [JsonPropertyName("nBoolJsonEnvClF")]
        public bool? NBoolJsonEnvClF { get; set; } = false;

        [CommandLine("-d", "--booljsonenvcl")]
        [EnvironmentVariable("BOOL_JSON_ENV_CL")]
        [JsonPropertyName("boolJsonEnvCl")]
        public bool BoolJsonEnvCl { get; set; }
    }
}
