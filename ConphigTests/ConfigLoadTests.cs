using System;
using ATornblad.Conphig;
using NUnit.Framework;

namespace ConphigTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = null;
        }

        [Test]
        public void BoolTests()
        {
            // Arrange

            // Act
            var defaultFileConfig = ATornblad.Conphig.Config.Load<BoolConfiguration>();

            // Assert
            Assert.IsFalse(defaultFileConfig.NBoolJsonEnvCl.Value);
            Assert.IsTrue(defaultFileConfig.NBoolJsonEnvClT.Value);
            Assert.IsFalse(defaultFileConfig.NBoolJsonEnvClF.Value);
            Assert.IsTrue(defaultFileConfig.BoolJsonEnvCl);

            // Act
            var noConfig = ATornblad.Conphig.Config.Load<BoolConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(noConfig.NBoolJsonEnvCl);
            Assert.IsTrue(noConfig.NBoolJsonEnvClT.Value);
            Assert.IsFalse(noConfig.NBoolJsonEnvClF.Value);
            Assert.IsFalse(noConfig.BoolJsonEnvCl);

            // Act
            var tfttConfig = ATornblad.Conphig.Config.Load<BoolConfiguration>("bool-configuration-tftt.json");

            // Assert
            Assert.IsTrue(tfttConfig.NBoolJsonEnvCl.Value);
            Assert.IsFalse(tfttConfig.NBoolJsonEnvClT.Value);
            Assert.IsTrue(tfttConfig.NBoolJsonEnvClF.Value);
            Assert.IsTrue(tfttConfig.BoolJsonEnvCl);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = () => new[] { "--nbooljsonenvclf", "--booljsonenvcl" };

            // Act
            var cliConfig = ATornblad.Conphig.Config.Load<BoolConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(cliConfig.NBoolJsonEnvCl);
            Assert.IsTrue(cliConfig.NBoolJsonEnvClT.Value);
            Assert.IsTrue(cliConfig.NBoolJsonEnvClF.Value);
            Assert.IsTrue(cliConfig.BoolJsonEnvCl);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = (key) => ("NBOOL_JSON_ENV_CL_T" == key) ? "false" : null;

            // Act
            var envConfig = ATornblad.Conphig.Config.Load<BoolConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(envConfig.NBoolJsonEnvCl);
            Assert.IsFalse(envConfig.NBoolJsonEnvClT.Value);
            Assert.IsFalse(envConfig.NBoolJsonEnvClF.Value);
            Assert.IsFalse(envConfig.BoolJsonEnvCl);
        }

        [Test]
        public void FloatTests()
        {
            // Arrange

            // Act
            var defaultFileConfig = ATornblad.Conphig.Config.Load<FloatConfiguration>();

            // Assert
            Assert.AreEqual(0.1f, defaultFileConfig.NFloatJsonEnvCl.Value);
            Assert.AreEqual(0.2f, defaultFileConfig.NFloatJsonEnvCl1.Value);
            Assert.AreEqual(0.3d, defaultFileConfig.NDoubleJsonEnvCl2.Value);
            Assert.AreEqual(0.4f, defaultFileConfig.FloatJsonEnvCl);
            Assert.AreEqual(0.5d, defaultFileConfig.DoubleJsonEnvCl3);

            // Act
            var noConfig = ATornblad.Conphig.Config.Load<FloatConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(noConfig.NFloatJsonEnvCl);
            Assert.AreEqual(1.1f, noConfig.NFloatJsonEnvCl1.Value);
            Assert.AreEqual(2.2d, noConfig.NDoubleJsonEnvCl2.Value);
            Assert.AreEqual(0.0f, noConfig.FloatJsonEnvCl);
            Assert.AreEqual(3.3d, noConfig.DoubleJsonEnvCl3);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = () => new[] { "--nfloatjsonenvcl1", "1.2", "--floatjsonenvcl", "2.3", "--doublejsonenvcl", "3.4" };

            // Act
            var cliConfig = ATornblad.Conphig.Config.Load<FloatConfiguration>("non-existing.json");

            // Assert
            Assert.AreEqual(1.2f, cliConfig.NFloatJsonEnvCl1.Value);
            Assert.AreEqual(2.3f, cliConfig.FloatJsonEnvCl);
            Assert.AreEqual(3.4d, cliConfig.DoubleJsonEnvCl3);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = (key) => ("NDOUBLE_JSON_ENV_CL_2" == key) ? "4.5" : null;

            // Act
            var envConfig = ATornblad.Conphig.Config.Load<FloatConfiguration>("non-existing.json");

            // Assert
            Assert.AreEqual(4.5d, envConfig.NDoubleJsonEnvCl2.Value);
        }

        [Test]
        public void IntTests()
        {
            // Arrange

            // Act
            var defaultFileConfig = ATornblad.Conphig.Config.Load<IntConfiguration>();

            // Assert
            Assert.AreEqual(3, defaultFileConfig.NIntJsonEnvCl.Value);
            Assert.AreEqual(1, defaultFileConfig.NIntJsonEnvCl1.Value);
            Assert.AreEqual(2L, defaultFileConfig.NLongJsonEnvCl2.Value);
            Assert.AreEqual(4, defaultFileConfig.IntJsonEnvCl);
            Assert.AreEqual(3L, defaultFileConfig.LongJsonEnvCl);

            // Act
            var noConfig = ATornblad.Conphig.Config.Load<IntConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(noConfig.NIntJsonEnvCl);
            Assert.AreEqual(1, noConfig.NIntJsonEnvCl1.Value);
            Assert.AreEqual(2L, noConfig.NLongJsonEnvCl2.Value);
            Assert.AreEqual(0, noConfig.IntJsonEnvCl);
            Assert.AreEqual(3L, noConfig.LongJsonEnvCl);

            // Act
            var _12n4Config = ATornblad.Conphig.Config.Load<IntConfiguration>("int-configuration-12n4.json");

            // Assert
            Assert.AreEqual(1, _12n4Config.NIntJsonEnvCl.Value);
            Assert.AreEqual(2, _12n4Config.NIntJsonEnvCl1.Value);
            Assert.IsNull(_12n4Config.NLongJsonEnvCl2);
            Assert.AreEqual(4, _12n4Config.IntJsonEnvCl);
            Assert.AreEqual(5, _12n4Config.LongJsonEnvCl);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = () => new[] { "--nintjsonenvcl1", "10", "--intjsonenvcl", "20", "--longjsonenvcl", "25" };

            // Act
            var cliConfig = ATornblad.Conphig.Config.Load<IntConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(cliConfig.NIntJsonEnvCl);
            Assert.AreEqual(10, cliConfig.NIntJsonEnvCl1.Value);
            Assert.AreEqual(2L, cliConfig.NLongJsonEnvCl2.Value);
            Assert.AreEqual(20, cliConfig.IntJsonEnvCl);
            Assert.AreEqual(25L, cliConfig.LongJsonEnvCl);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = (key) => ("NLONG_JSON_ENV_CL_2" == key) ? "30" : ("LONG_JSON_ENV_CL" == key) ? "35" : null;

            // Act
            var envConfig = ATornblad.Conphig.Config.Load<IntConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(envConfig.NIntJsonEnvCl);
            Assert.AreEqual(1, envConfig.NIntJsonEnvCl1.Value);
            Assert.AreEqual(30L, envConfig.NLongJsonEnvCl2.Value);
            Assert.AreEqual(0, envConfig.IntJsonEnvCl);
            Assert.AreEqual(35L, envConfig.LongJsonEnvCl);
        }

        [Test]
        public void StringTests()
        {
            // Arrange

            // Act
            var defaultFileConfig = ATornblad.Conphig.Config.Load<StringConfiguration>();

            // Assert
            Assert.AreEqual("from-file-first", defaultFileConfig.FirstNoDefault);
            Assert.AreEqual("from-file-second", defaultFileConfig.SecondDefaultX);
            Assert.AreEqual("from-file-third", defaultFileConfig.ThirdDefaultY);

            // Act
            var noConfig = ATornblad.Conphig.Config.Load<StringConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(noConfig.FirstNoDefault);
            Assert.AreEqual("x", noConfig.SecondDefaultX);
            Assert.AreEqual("y", noConfig.ThirdDefaultY);

            // Act
            var abnConfig = ATornblad.Conphig.Config.Load<StringConfiguration>("string-configuration-abnull.json");

            // Assert
            Assert.AreEqual("a", abnConfig.FirstNoDefault);
            Assert.AreEqual("b", abnConfig.SecondDefaultX);
            Assert.IsNull(abnConfig.ThirdDefaultY);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = () => new[] { "--second", "from-cli" };

            // Act
            var cliConfig = ATornblad.Conphig.Config.Load<StringConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(cliConfig.FirstNoDefault);
            Assert.AreEqual("from-cli", cliConfig.SecondDefaultX);
            Assert.AreEqual("y", cliConfig.ThirdDefaultY);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = (key) => ("STRING_THIRD" == key) ? "from-env" : null;

            // Act
            var envConfig = ATornblad.Conphig.Config.Load<StringConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(envConfig.FirstNoDefault);
            Assert.AreEqual("x", envConfig.SecondDefaultX);
            Assert.AreEqual("from-env", envConfig.ThirdDefaultY);
        }

        [Test]
        public void DateTimeTests()
        {
            // Arrange

            // Act
            var noconfig = ATornblad.Conphig.Config.Load<DateTimeConfiguration>("non-existing.json");

            // Assert
            Assert.IsNull(noconfig.NDTDefNull);
            Assert.IsNull(noconfig.NDTODefNull);
            Assert.AreEqual(new DateTime(2020, 2, 3, 4, 5, 6, DateTimeKind.Utc), noconfig.NDTDefValue.Value);
            Assert.AreEqual(new DateTimeOffset(2020, 2, 3, 4, 5, 6, TimeSpan.Zero), noconfig.NDTODefValue.Value);
            Assert.AreEqual(new DateTime(2021, 2, 3, 4, 5, 6, DateTimeKind.Utc), noconfig.DTDefValue);
            Assert.AreEqual(new DateTimeOffset(2021, 2, 3, 4, 5, 6, TimeSpan.Zero), noconfig.DTODefValue);

            // Act
            var defaultfile = ATornblad.Conphig.Config.Load<DateTimeConfiguration>();

            // Assert
            Assert.AreEqual(new DateTime(2021, 9, 28, 19, 0, 0, DateTimeKind.Utc), defaultfile.NDTDefNull.Value);
            Assert.IsNull(defaultfile.NDTDefValue);
            Assert.AreEqual(new DateTimeOffset(2021, 12, 24, 15, 0, 0, TimeSpan.FromMinutes(120)), defaultfile.DTODefValue);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = () => new[] { "--ndtdefvalue", "1990-01-02T03:04:05Z", "--ndtodefnull", "1980-12-31T23:59:59+01:00" };

            // Act
            var cli = ATornblad.Conphig.Config.Load<DateTimeConfiguration>();

            // Assert
            Assert.AreEqual(new DateTime(1990, 1, 2, 3, 4, 5, DateTimeKind.Utc), cli.NDTDefValue);
            Assert.AreEqual(new DateTimeOffset(1980, 12, 31, 23, 59, 59, TimeSpan.FromMinutes(60)), cli.NDTODefNull);

            // Arrange
            ATornblad.Conphig.Config.CommandLineArgsGetter = null;
            ATornblad.Conphig.Config.EnvironmentVariableGetter = (key) => (key == "NDT_DEF_NULL") ? "1974-05-28T10:00:00Z" : null;

            // Act
            var env = ATornblad.Conphig.Config.Load<DateTimeConfiguration>();

            // Assert
            Assert.AreEqual(new DateTime(1974, 5, 28, 10, 0, 0, DateTimeKind.Utc), env.NDTDefNull);
        }

        [Test]
        public void ArrayTests()
        {
            // Arrange

            // Act
            var fileConfig = Config.Load<ArrayConfiguration>();

            // Assert
            Assert.AreEqual(new[] { "a", "b", "c" }, fileConfig.Strings);
        }

        [Test]
        public void PrecedenceTests()
        {
            // TODO: Test all possible combinations of default value, file value, environment value and command line value
        }
    }
}
