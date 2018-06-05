using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Moq;
using PlaylistManager.Controllers;
using PlaylistManager.Models;
using Xunit;
using Xunit.Abstractions;

namespace XUnitPlaylistManagerTests
{
    public class MusicLibraryControllerTest
    {
        private XunitLogger<MusicLibraryController> _logger;

        public MusicLibraryControllerTest(ITestOutputHelper output)
        {
            _logger = new XunitLogger<MusicLibraryController>(output);
        }

        #region PlayListDeleteTrack tests

        [Fact]
        public void PlayListDeleteTrack_Success()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            var testOutput = new List<TrackInfo>() { new TrackInfo() {  Title = "Final Countdown",
                                                                        Album = "Greatest Hits",
                                                                        Artist = "Europe",
                                                                        TrackId = 6,
                                                                        TrackNum = 1 } };
            mockMusicRepository.Setup(x => x.PlayListDeleteTrack(42, 7)).Returns(testOutput);

            // Execute:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var result  = _musicLibraryController.PlayListDeleteTrack(42, 7);

            // Test:
            Assert.Equal(result, testOutput);
        }


        [Fact]
        public void PlayListDeleteTrack_Fail()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            List<TrackInfo> testOutput = null;
            mockMusicRepository.Setup(x => x.PlayListDeleteTrack(43, 5)).Returns(testOutput);

            // Execute:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var result = _musicLibraryController.PlayListDeleteTrack(43, 5);

            // Test:
            Assert.Equal(result, testOutput);
        }

        #endregion // PlayListDeleteTrack tests

        #region DeletePlayList tests

        // Two tests in one to handle both code paths, utilizing [Theory] test attribute:
        [Theory]
        [InlineData(9)]
        [InlineData(80)]
        public void DeletePlayList_tests(int value)
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            mockMusicRepository.Setup(x => x.DeletePlayList(value)).Returns(value == 9 ? true : false);

            // Execute:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var result = _musicLibraryController.DeletePlayList(value);

            // Test:
            Assert.Equal(result, (value == 9 ? true : false));
        }

        #endregion // DeletePlayList tests

        #region AllPlaylists tests

        [Fact]
        public void AllPlaylists_InvalidPageNum()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();

            // Execute / Test:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var ex = Assert.Throws<ArgumentException>("pageNum", () => _musicLibraryController.AllPlaylists(0, 5));
            Assert.StartsWith("Value must be greater than 0", ex.Message);
        }

        [Fact]
        public void AllPlaylists_InvalidPageSize()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();

            // Execute / Test:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var ex = Assert.Throws<ArgumentException>("pageSize", () => _musicLibraryController.AllPlaylists(3, 0));
            Assert.StartsWith("Value must be greater than 0", ex.Message);
        }

        [Fact]
        public void AllPlaylists_ValidArgs()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            var testOutput = new List<PlaylistSummary>() { new PlaylistSummary() { Name = "Rock Hits" }, new PlaylistSummary() { Name = "80's Pop" } };
            mockMusicRepository.Setup(x => x.GetAllPlaylists(2, 10)).Returns(testOutput);

            // Execute:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var result = _musicLibraryController.AllPlaylists(2, 10);

            // Test:
            Assert.Equal(result, testOutput);
        }

        #endregion // AllPlaylists tests

        #region // SearchTracks tests

        [Theory]
        [InlineData("o", "", "")]
        [InlineData("Sh", "", "")]
        [InlineData("ll", "", "")]
        public void SearchTracks_OK(string title, string artist, string album)
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            var testData = new List<TrackInfo>() { new TrackInfo() { Title = "Hello" }, new TrackInfo() { Title = "Shout" } };
            mockMusicRepository.Setup(x => x.SearchTracks(title,artist,album,50)).Returns(testData.FindAll(x => x.Title.Contains(title)));

            // Execute:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var result = _musicLibraryController.SearchTracks(title, artist, album);

            // Test:
            Assert.Equal(result, testData.FindAll(x => x.Title.Contains(title)));
        }

        [Fact]
        public void SearchTracks_SqlException()
        {
            // Arrange:
            var mockMusicRepository = new Mock<IMusicRepository>();
            var exceptionOutput = MakeSqlException();
            mockMusicRepository.Setup(x => x.SearchTracks("", "Nirvana", "Nevermind", 50)).Throws(exceptionOutput);

            // Execute & Test:
            var _musicLibraryController = new MusicLibraryController(mockMusicRepository.Object, _logger);
            var ex = Assert.Throws<SqlException>(() => _musicLibraryController.SearchTracks("", "Nirvana", "Nevermind"));
            Assert.Equal(exceptionOutput, ex); // Exception should bubble up through the controller method
        }

        #endregion // SearchTracks tests

        #region Utility methods

        private SqlException MakeSqlException()
        {
            SqlException exception = null;
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
                conn.Open();
            }
            catch (SqlException ex)
            {
                exception = ex;
            }
            return exception;
        }

        #endregion // Utility methods
    }
}
