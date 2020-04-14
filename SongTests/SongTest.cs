using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MixableRangeImplementation;
using MixableRangeInterface;
using SongImplementation;
using SongInterface;
using XmlWrapperImplementation;
using XmlWrapperInterface;

namespace SongTests
{
    [TestClass]
    public class SongTest
    {

        [TestMethod]
        public void IsInTempoRange_BelowFastestAndSlowestTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(131.001, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128.000;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInTempoRange_AboveFastestAndSlowestTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(124.999, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128.000;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInTempoRange_BelowFastestDoubleAndSlowestDoubleTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(65.501, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128.000;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInTempoRange_AboveFastestDoubleAndSlowestDoubleTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(62.499, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInTempoRange_BelowFastestHalfAndSlowestHalfTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(262.002, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128.000;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInTempoRange_AboveFastestHalfAndSlowestHalfTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            tempoRange.Load(249.998, 3);
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingTempo = 128;

            // Act
            var result = song.IsInTempoRange(trailingTempo);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
