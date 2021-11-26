using System;
using System.IO;
using ATornblad.Conphig;
using NUnit.Framework;

namespace ConphigHelpTests
{
    public class ConsoleWriteTests
    {
        [Test]
        public void EmptyConfigTest()
        {
            // Arrange
            var writer = new StringWriter();
            string programName = "PROGRAM";
            string nl = System.Environment.NewLine;

            // Act
            ConfigHelp.WriteTo<EmptyConfig>(writer, programName);

            // Assert
            string output = writer.ToString();
            Assert.AreEqual($"USAGE: {programName}{nl}", output);
        }

        [Test]
        public void KitchenSinkConfigTest()
        {
            // Arrange
            var writer = new StringWriter();
            string programName = "PROGRAM";
            string nl = System.Environment.NewLine;

            // Act
            ConfigHelp.WriteTo<KitchenSinkConfig>(writer, programName);

            // Assert
            string output = writer.ToString();
            Assert.AreEqual($"USAGE: {programName} [-bf] [-n NUMBER --only-long-bool --other-number NUMBER -t TEXT]{nl}", output);
        }
    }
}