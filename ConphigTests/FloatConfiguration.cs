using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class FloatConfiguration
    {
        [CommandLine("--nfloatjsonenvcl")]
        [EnvironmentVariable("NFLOAT_JSON_ENV_CL")]
        [JsonPropertyName("nFloatJsonEnvCl")]
        public float? NFloatJsonEnvCl { get; set; }

        [Default(1.1f)]
        [CommandLine("--nfloatjsonenvcl1")]
        [EnvironmentVariable("NFLOAT_JSON_ENV_CL_1")]
        [JsonPropertyName("nFloatJsonEnvCl1")]
        public float? NFloatJsonEnvCl1 { get; set; }

        [Default(2.2d)]
        [CommandLine("--ndoublejsonenvcl2")]
        [EnvironmentVariable("NDOUBLE_JSON_ENV_CL_2")]
        [JsonPropertyName("nDoubleJsonEnvCl2")]
        public double? NDoubleJsonEnvCl2 { get; set; }

        [CommandLine("--floatjsonenvcl")]
        [EnvironmentVariable("FLOAT_JSON_ENV_CL")]
        [JsonPropertyName("floatJsonEnvCl")]
        public float FloatJsonEnvCl { get; set; }

        [Default(3.3d)]
        [CommandLine("--doublejsonenvcl")]
        [EnvironmentVariable("DOUBLE_JSON_ENV_CL")]
        [JsonPropertyName("doubleJsonEnvCl")]
        public double DoubleJsonEnvCl3 { get; set; }
    }
}
