using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MixableRangeImplementation;
using MixableRangeInterface;

namespace MixableRangeTests
{
    [TestClass]
    public class IntensityRangeTest
    {
        [TestMethod]
        public void Load_Test()
        {
            // Arrange
            IIntensityRange intensityRange = new IntensityRange();

            // Act
            intensityRange.Load(7);

            // Assert
            Assert.AreEqual(8, intensityRange.PlusOneIntensity);
            Assert.AreEqual(6, intensityRange.MinusOneIntensity);
        }
    }
}
