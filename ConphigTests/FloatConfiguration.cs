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

        [CommandLine("--nfloatjsonenvcl1")]
        [EnvironmentVariable("NFLOAT_JSON_ENV_CL_1")]
        [JsonPropertyName("nFloatJsonEnvCl1")]
        public float? NFloatJsonEnvCl1 { get; set; } = 1.1f;

        [CommandLine("--ndoublejsonenvcl2")]
        [EnvironmentVariable("NDOUBLE_JSON_ENV_CL_2")]
        [JsonPropertyName("nDoubleJsonEnvCl2")]
        public double? NDoubleJsonEnvCl2 { get; set; } = 2.2d;

        [CommandLine("--floatjsonenvcl")]
        [EnvironmentVariable("FLOAT_JSON_ENV_CL")]
        [JsonPropertyName("floatJsonEnvCl")]
        public float FloatJsonEnvCl { get; set; }

        [CommandLine("--doublejsonenvcl")]
        [EnvironmentVariable("DOUBLE_JSON_ENV_CL")]
        [JsonPropertyName("doubleJsonEnvCl")]
        public double DoubleJsonEnvCl3 { get; set; } = 3.3d;
    }
}
