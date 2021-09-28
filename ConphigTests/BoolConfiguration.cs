﻿using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class BoolConfiguration
    {
        [CommandLine("--nbooljsonenvcl")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL")]
        [JsonPropertyName("nBoolJsonEnvCl")]
        public bool? NBoolJsonEnvCl { get; set; }

        [Default(true)]
        [CommandLine("--nbooljsonenvclt")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL_T")]
        [JsonPropertyName("nBoolJsonEnvClT")]
        public bool? NBoolJsonEnvClT { get; set; }

        [Default(false)]
        [CommandLine("--nbooljsonenvclf")]
        [EnvironmentVariable("NBOOL_JSON_ENV_CL_F")]
        [JsonPropertyName("nBoolJsonEnvClF")]
        public bool? NBoolJsonEnvClF { get; set; }

        [CommandLine("--booljsonenvcl")]
        [EnvironmentVariable("BOOL_JSON_ENV_CL")]
        [JsonPropertyName("boolJsonEnvCl")]
        public bool BoolJsonEnvCl { get; set; }
    }
}
