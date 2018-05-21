﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlaylistManager.Models;

namespace PlaylistManager.Controllers
{
    [Produces("application/json")]
    [Route("api/MusicLibrary")]
    public class MusicLibraryController : Controller
    {
        private IMusicRepository _musicRepository;
        private readonly ILogger _logger;

        public MusicLibraryController(IMusicRepository musicRepository,
                                      ILogger<MusicLibraryController> logger)
        {
            _musicRepository = musicRepository;
            _logger = logger;
        }

        // GET: api/MusicLibrary/CountAllTracks
        [HttpGet("[action]")]
        public int CountAllTracks()
        {
            var num = _musicRepository.AllTracksCount;
            //_logger.LogInformation("Count All tracks method called - counted {0} tracks", num);
            return num;
        }

        /// <summary>
        /// GET: api/MusicLibrary/AllTracks (server side paged results)
        /// </summary>
        /// <param name="pageNum">Page number to get</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of music track info classes</returns>
        [HttpGet("[action]")]
        public IEnumerable<TrackInfo> AllTracks(int pageNum, int pageSize)
        {
            return _musicRepository.GetAllTracks(pageNum, pageSize);
        }

        /// <summary>
        /// POST: api/MusicLibrary/AddPlayList
        /// </summary>
        /// <param name="name">The name of the new playlist</param>
        [HttpPost("[action]")]
        public int AddPlayList([FromBody]PlaylistInfo pl)
        {
            return _musicRepository.AddPlayList(pl.Name);
        }

        // GET: api/MusicLibrary/CountAllPlaylists
        [HttpGet("[action]")]
        public int CountAllPlaylists()
        {
            var num = _musicRepository.AllPlaylistsCount;
            return num;
        }

        /// <summary>
        /// GET: api/MusicLibrary/AllPlaylists (server side paged results)
        /// </summary>
        /// <param name="pageNum">Page number to get</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of music playlist info classes</returns>
        [HttpGet("[action]")]
        public IEnumerable<PlaylistInfo> AllPlaylists(int pageNum, int pageSize)
        {
            return _musicRepository.GetAllPlaylists(pageNum, pageSize);
        }

        // GET: api/MusicLibrary/GetPlaylist/5
        [HttpGet("[action]/{id}")]
        public PlaylistDetails Playlist(int id)
        {
            var pl = _musicRepository.GetPlaylist(id);
            if (pl == null)
                _logger.LogError("Playlist with ID {} requested but not found in DB", id);
            return pl;
        }

        /// <summary>
        /// GET: api/MusicLibrary/SearchTracks (server side paged results)
        /// </summary>
        /// <param name="title">keywords to search track title</param>
        /// <param name="artist">keywords to search track artist</param>
        /// <param name="album">keywords to search track album</param>
        /// <returns>List of top 50 music tracks that match the search terms</returns>
        [HttpGet("[action]")]
        public IEnumerable<TrackInfo> SearchTracks(string title, string artist, string album)
        {
            return _musicRepository.SearchTracks(title, artist, album, 50);
        }

        // PUT: api/MusicLibrary/UpdatePlayListTitle/5
        [HttpPut("[action]/{id}")]
        public PlaylistSummary UpdatePlayListTitle(int id, [FromBody]PlaylistDetails playlist)
        {
            var pl = _musicRepository.UpdatePlayListTitle(id, playlist.Name);
            if (pl == null)
                _logger.LogError("Playlist with ID {} requested but not found in DB", id);
            return pl;
        }


        ////////////////////////////////////////////////////////////////////



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
