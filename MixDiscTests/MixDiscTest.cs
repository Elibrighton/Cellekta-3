using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MixableRangeImplementation;
using MixableRangeInterface;
using MixDiscImplementation;
using MixDiscInterface;
using SongImplementation;
using SongInterface;
using XmlWrapperImplementation;
using XmlWrapperInterface;

namespace MixDiscTests
{
    [TestClass]
    public class MixDiscTest
    {
        [TestMethod]
        public void GetBestMatch_SongsAreSame_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper1 = new XmlWrapper();
            ITempoRange tempoRange1 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange1 = new HarmonicKeyRange();
            IIntensityRange intensityRange1 = new IntensityRange();
            ISong song1 = new Song(xmlWrapper1, tempoRange1, harmonicKeyRange1, intensityRange1);

            var baseTrackList = new List<ISong>
            {
                song1
            };

            var playlistTracks = new List<ISong>
            {
                song1
            };

            var intensityStyle = "Random";
            var minPlaytime = 200;
            var mixLength = 0;

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            // Act
            var result = mixDisc.GetBestMatch();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetBestMatch_SongAlreadyInMixableTrackCombination_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper1 = new XmlWrapper();
            ITempoRange tempoRange1 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange1 = new HarmonicKeyRange();
            IIntensityRange intensityRange1 = new IntensityRange();
            ISong song1 = new Song(xmlWrapper1, tempoRange1, harmonicKeyRange1, intensityRange1)
            {
                Artist = "Song 1",
                PlayTime = 100
            };

            IXmlWrapper xmlWrapper2 = new XmlWrapper();
            ITempoRange tempoRange2 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange2 = new HarmonicKeyRange();
            IIntensityRange intensityRange2 = new IntensityRange();
            ISong song2 = new Song(xmlWrapper2, tempoRange2, harmonicKeyRange2, intensityRange2)
            {
                Artist = "Song 2",
                PlayTime = 100
            };

            var baseTrackList = new List<ISong>
            {
                song2,
                song1
            };

            var playlistTracks = new List<ISong>
            {
                song2
            };

            var intensityStyle = "Random";
            var minPlaytime = 300;
            var mixLength = 0;

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            // Act
            var result = mixDisc.GetBestMatch();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetBestMatch_MatchOnTempo_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper1 = new XmlWrapper();
            ITempoRange tempoRange1 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange1 = new HarmonicKeyRange();
            IIntensityRange intensityRange1 = new IntensityRange();
            ISong song1 = new Song(xmlWrapper1, tempoRange1, harmonicKeyRange1, intensityRange1)
            {
                Artist = "Song 1",
                PlayTime = 100,
                TrailingTempo = 128.000
            };

            IXmlWrapper xmlWrapper2 = new XmlWrapper();
            ITempoRange tempoRange2 = new TempoRange();
            tempoRange2.Load(128, 3);
            IHarmonicKeyRange harmonicKeyRange2 = new HarmonicKeyRange();
            IIntensityRange intensityRange2 = new IntensityRange();
            ISong song2 = new Song(xmlWrapper2, tempoRange2, harmonicKeyRange2, intensityRange2)
            {
                Artist = "Song 2",
                PlayTime = 100
            };

            var baseTrackList = new List<ISong>
            {
                song1
            };

            var playlistTracks = new List<ISong>
            {
                song2
            };

            var intensityStyle = "Random";
            var minPlaytime = 200;
            var mixLength = 0;

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            // Act
            var result = mixDisc.GetBestMatch();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void GetBestMatch_MatchOnHarmonicKey_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper1 = new XmlWrapper();
            ITempoRange tempoRange1 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange1 = new HarmonicKeyRange();
            IIntensityRange intensityRange1 = new IntensityRange();
            ISong song1 = new Song(xmlWrapper1, tempoRange1, harmonicKeyRange1, intensityRange1)
            {
                Artist = "Song 1",
                PlayTime = 100,
                TrailingTempo = 128.000,
                TrailingHarmonicKey = "8A"
            };

            IXmlWrapper xmlWrapper2 = new XmlWrapper();
            ITempoRange tempoRange2 = new TempoRange();
            tempoRange2.Load(128, 3);
            IHarmonicKeyRange harmonicKeyRange2 = new HarmonicKeyRange();
            harmonicKeyRange2.Load("8A");
            IIntensityRange intensityRange2 = new IntensityRange();
            ISong song2 = new Song(xmlWrapper2, tempoRange2, harmonicKeyRange2, intensityRange2)
            {
                Artist = "Song 2",
                PlayTime = 100
            };

            var baseTrackList = new List<ISong>
            {
                song1
            };

            var playlistTracks = new List<ISong>
            {
                song2
            };

            var intensityStyle = "Random";
            var minPlaytime = 200;
            var mixLength = 0;

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            // Act
            var result = mixDisc.GetBestMatch();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void GetBestMatch_MatchOnIntensity_Test()
        {
            // Arrange
            IXmlWrapper xmlWrapper1 = new XmlWrapper();
            ITempoRange tempoRange1 = new TempoRange();
            IHarmonicKeyRange harmonicKeyRange1 = new HarmonicKeyRange();
            IIntensityRange intensityRange1 = new IntensityRange();
            ISong song1 = new Song(xmlWrapper1, tempoRange1, harmonicKeyRange1, intensityRange1)
            {
                Artist = "Song 1",
                PlayTime = 100,
                TrailingTempo = 128.000,
                TrailingHarmonicKey = "8A",
                Intensity = 7
            };

            IXmlWrapper xmlWrapper2 = new XmlWrapper();
            ITempoRange tempoRange2 = new TempoRange();
            tempoRange2.Load(128, 3);
            IHarmonicKeyRange harmonicKeyRange2 = new HarmonicKeyRange();
            harmonicKeyRange2.Load("8A");
            IIntensityRange intensityRange2 = new IntensityRange();
            intensityRange2.Load(7);
            ISong song2 = new Song(xmlWrapper2, tempoRange2, harmonicKeyRange2, intensityRange2)
            {
                Artist = "Song 2",
                PlayTime = 100,
                Intensity = 7
            };

            var baseTrackList = new List<ISong>
            {
                song1
            };

            var playlistTracks = new List<ISong>
            {
                song2
            };

            var intensityStyle = "Random";
            var minPlaytime = 200;
            var mixLength = 0;

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            // Act
            var result = mixDisc.GetBestMatch();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }
    }
}
