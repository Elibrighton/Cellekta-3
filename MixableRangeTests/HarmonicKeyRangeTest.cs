using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MixableRangeImplementation;
using MixableRangeInterface;

namespace MixableRangeTests
{
    [TestClass]
    public class HarmonicKeyRangeTest
    {
        [TestMethod]
        public void Load_Test()
        {
            // Arrange
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();

            // Act
            harmonicKeyRange.Load("8A");

            // Assert
            Assert.AreEqual("8A", harmonicKeyRange.InnerCircleHarmonicKey);
            Assert.AreEqual("8B", harmonicKeyRange.OuterCircleHarmonicKey);
            Assert.AreEqual("7A", harmonicKeyRange.MinusOneHarmonicKey);
            Assert.AreEqual("9A", harmonicKeyRange.PlusOneHarmonicKey);
        }

        [TestMethod]
        public void Load_AtStart_Test()
        {
            // Arrange
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();

            // Act
            harmonicKeyRange.Load("1A");

            // Assert
            Assert.AreEqual("1A", harmonicKeyRange.InnerCircleHarmonicKey);
            Assert.AreEqual("1B", harmonicKeyRange.OuterCircleHarmonicKey);
            Assert.AreEqual("12A", harmonicKeyRange.MinusOneHarmonicKey);
            Assert.AreEqual("2A", harmonicKeyRange.PlusOneHarmonicKey);
        }

        [TestMethod]
        public void Load_AtEnd_Test()
        {
            // Arrange
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();

            // Act
            harmonicKeyRange.Load("12B");

            // Assert
            Assert.AreEqual("12A", harmonicKeyRange.InnerCircleHarmonicKey);
            Assert.AreEqual("12B", harmonicKeyRange.OuterCircleHarmonicKey);
            Assert.AreEqual("11B", harmonicKeyRange.MinusOneHarmonicKey);
            Assert.AreEqual("1B", harmonicKeyRange.PlusOneHarmonicKey);
        }
    }
}
