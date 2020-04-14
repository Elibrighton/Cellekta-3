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

        [TestMethod]
        public void IsInHarmonicKeyRange_PlusOneAndOppositeCircle_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            harmonicKeyRange.Load("9B");
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingHarmonicKey = "8A";

            // Act
            var result = song.IsInHarmonicKeyRange(trailingHarmonicKey);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInHarmonicKeyRange_PlusTwo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            harmonicKeyRange.Load("10A");
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingHarmonicKey = "8A";

            // Act
            var result = song.IsInHarmonicKeyRange(trailingHarmonicKey);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInHarmonicKeyRange_MinusTwo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            harmonicKeyRange.Load("6B");
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingHarmonicKey = "8B";

            // Act
            var result = song.IsInHarmonicKeyRange(trailingHarmonicKey);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInHarmonicKeyRange_MinusOneAndOppositeCircle_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            harmonicKeyRange.Load("7B");
            IIntensityRange intensityRange = new IntensityRange();
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",

            };

            var trailingHarmonicKey = "8A";

            // Act
            var result = song.IsInHarmonicKeyRange(trailingHarmonicKey);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInIntensityRange_BelowMinusOne_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            intensityRange.Load(5);
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",
                Intensity = 5

            };

            var trailingIntensity = 7;

            // Act
            var result = song.IsInIntensityRange(trailingIntensity);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsInIntensityRange_AbovePlusOne_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper = new XmlWrapper();
            ITempoRange tempoRange = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IIntensityRange intensityRange = new IntensityRange();
            intensityRange.Load(9);
            ISong song = new Song(xmlWrapper, tempoRange, harmonicKeyRange, intensityRange)
            {
                Artist = "Song 1",
                Intensity = 9

            };

            var trailingIntensity = 7;

            // Act
            var result = song.IsInIntensityRange(trailingIntensity);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
