using System;
using ATornblad.Conphig.Internals;
using NUnit.Framework;

namespace ConphigTests
{
    public class ConversionTests
    {
        [Test]
        public void SameTypeTests()
        {
            // Arrange
            var stringIn = "string";
            var intIn = 123;
            var dateIn = new DateTime(2000, 1, 1, 0, 0, 0);
            var confIn = new BoolConfiguration();

            // Act
            var stringOut = Conversion.ChangeType(stringIn, typeof(string));
            var intOut = Conversion.ChangeType(intIn, typeof(int));
            var dateOut = Conversion.ChangeType(dateIn, typeof(DateTime));
            var confOut = Conversion.ChangeType(confIn, typeof(BoolConfiguration));

            // Assert
            Assert.That(stringOut, Is.EqualTo(stringOut));
            Assert.That(intOut, Is.EqualTo(intIn));
            Assert.That(dateOut, Is.EqualTo(dateIn));
            Assert.That(confOut, Is.SameAs(confIn));
        }

        [Test]
        public void NullTests()
        {
            // Arrange

            // Act
            var stringOut = Conversion.ChangeType(null, typeof(string));
            var intOut = Conversion.ChangeType(null, typeof(int?));
            var dateOut = Conversion.ChangeType(null, typeof(DateTime?));
            var confOut = Conversion.ChangeType(null, typeof(BoolConfiguration));

            // Assert
            Assert.That(stringOut, Is.Null);
            Assert.That(intOut, Is.Null);
            Assert.That(dateOut, Is.Null);
            Assert.That(confOut, Is.Null);
        }
    }
}
