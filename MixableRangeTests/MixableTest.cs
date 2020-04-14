using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MixableRangeImplementation;
using MixableRangeInterface;

namespace MixableRangeTests
{
    [TestClass]
    public class MixableTest
    {
        [TestMethod]
        public void TempoRange_Load_Test()
        {
            // Arrange
            ITempoRange tempoRange = new TempoRange();

            // Act
            tempoRange.Load(128.000, 3);

            // Assert
            Assert.AreEqual(131.000, tempoRange.FastestTempo);
            Assert.AreEqual(125.000, tempoRange.SlowestTempo);
            Assert.AreEqual(259.000, tempoRange.FastestDoubleTempo);
            Assert.AreEqual(253.000, tempoRange.SlowestDoubleTempo);
            Assert.AreEqual(67.000, tempoRange.FastestHalfTempo);
            Assert.AreEqual(61.000, tempoRange.SlowestHalfTempo);
        }
    }
}
