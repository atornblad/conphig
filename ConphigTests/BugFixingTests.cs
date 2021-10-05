using System;
using ATornblad.Conphig;
using NUnit.Framework;

namespace ConphigTests
{
    public class BugFixingTests
    {
        [SetUp]
        public void Setup()
        {
            Config.CommandLineArgsGetter = null;
            Config.EnvironmentVariableGetter = null;
        }

        [Test(Author ="Anders Marzi Tornblad", Description = "2021-10-05 Non-boolean switches without value at the end of the command line crashes")]
        public void CommandLineIndexOutOfRangeSimpleBug()
        {
            // Arrange
            Config.CommandLineArgsGetter = () => new[] { "--switch" };

            // Act
            var config = Config.Load<CommandLineIndexOutOfRangeBugConfig>();

            // Assert
            Assert.IsNull(config.Value);
        }

        [Test(Author = "Anders Marzi Tornblad", Description = "2021-10-05 Array switches without value at the end of the command line crashes")]
        public void CommandLineIndexOutOfRangeArrayBug()
        {
            // Arrange
            Config.CommandLineArgsGetter = () => new[] { "--array", "a", "--array" };

            // Act
            var config = Config.Load<CommandLineIndexOutOfRangeBugConfig>();

            // Assert
            Assert.AreEqual(new[] { "a" }, config.Values);
        }

        public class CommandLineIndexOutOfRangeBugConfig
        {
            [CommandLine("--switch")]
            public string Value { get; set; }

            [CommandLine("--array")]
            public string[] Values { get; set; }
        }
    }
}
