using DealerOnCodeChallenge.Utility;
using NUnit.Framework;

namespace DealerOnCodeChallenge.Unit.Test.Utility
{
    public class DoubleExtensionTest

    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(45.21, 45.25)]
        [TestCase(2.01, 2.05)]
        [TestCase(.08, .10)]
        [TestCase(10.00, 10.00)]
        [TestCase(0.00, 0.00)]
        [TestCase(.96, 1.00)]
        [TestCase(-1.08, -1.05)]
        [TestCase(-.01, 0.00)]
        [TestCase(-99.9, -99.90)]
        public void GoUpToNearest5Hundredths_variance(double value, double expected)
        {
            var result = value.GoUpToNearest5Hundredths();

            Assert.AreEqual(expected, result);
        }
    }
}