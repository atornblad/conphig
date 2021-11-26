using ATornblad.Conphig;

namespace ConphigHelpTests
{
    public class KitchenSinkConfig
    {
        [CommandLine("-t", "--text")]
        public string Text { get; set; }

        [CommandLine("-n", "--number")]
        public int Number { get; set; }

        [CommandLine("--other-number")]
        public int? OtherNumber { get; set; }

        [CommandLine("-b", "--bool")]
        public bool YesOrNo { get; set; }

        [CommandLine("-f", "--flag")]
        public bool Flag { get; set; }

        [CommandLine("--only-long-bool")]
        public bool LongBool { get; set; }
    }
}