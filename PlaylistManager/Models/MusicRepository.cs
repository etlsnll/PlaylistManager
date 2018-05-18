﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public class MusicRepository : IMusicRepository
    {
        private MusicLibraryContext _dbContext;

        public MusicRepository(MusicLibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TrackInfo> GetAllTracks(int pageNum, int pageSize)
        {
            var result = _dbContext.Tracks.OrderBy(t => t.Album.Title)
                                           .ThenBy(t => t.DiscNum ?? 1)
                                           .ThenBy(t => t.TrackNum ?? 1)
                                           .Skip((pageNum - 1) * pageSize)
                                           .Take(pageSize)
                                           .Select(t => new TrackInfo
                                            {
                                                TrackId = t.TrackId,
                                                Album = t.Album.Title,
                                                AlbumArtist = t.Album.AlbumArtist,
                                                Artist = t.Artist.Title,
                                                Year = t.Album.Year,
                                                Title = t.Title,
                                                TrackNum = t.TrackNum
                                            });
            return result;
        }

        public int AllTracksCount
        {
            get
            {
                var result = _dbContext.Tracks.Count();
                return result;
            }
        }

        public int AddPlayList(string title)
        { 
            var pl = new Playlist() { Title = title };
            _dbContext.Playlists.Add(pl);
            _dbContext.SaveChanges();
            return pl.PlaylistId;
        }

        public int AllPlaylistsCount
        {
            get
            {
                var result = _dbContext.Playlists.Count();
                return result;
            }
        }

        public IEnumerable<PlaylistSummary> GetAllPlaylists(int pageNum, int pageSize)
        {
            var result = _dbContext.Playlists.OrderBy(p => p.Title)
                                             .Skip((pageNum - 1) * pageSize)
                                             .Take(pageSize)
                                             .Select(p => new PlaylistSummary
                                             {
                                                 Id = p.PlaylistId,
                                                 Name = p.Title,
                                                 NumTracks = p.PlaylistTracks.Count()
                                             });
            return result;
        }

        /// <summary>
        /// Get data about a playlist
        /// </summary>
        /// <param name="id">The ID of the list to retrieve</param>
        /// <returns>A model of the data, or null if not found</returns>
        public PlaylistDetails GetPlaylist(int id)
        {
            PlaylistDetails result = null;
            var p = _dbContext.Playlists.FirstOrDefault(x => x.PlaylistId == id);

            if (p != null)
            {
                result = new PlaylistDetails
                {
                    Id = p.PlaylistId,
                    Name = p.Title                    
                };
                if (p.PlaylistTracks != null)
                    result.Tracks = p.PlaylistTracks.OrderBy(x => x.TrackNum)
                                             .Select(t => new TrackInfo
                                             {
                                                 TrackId = t.PlaylistTrackId,
                                                 Album = t.Track.Album.Title,
                                                 AlbumArtist = t.Track.Album.AlbumArtist,
                                                 Artist = t.Track.Artist.Title,
                                                 Year = t.Track.Album.Year,
                                                 Title = t.Track.Title,
                                                 TrackNum = t.TrackNum
                                             });
                else
                    result.Tracks = new List<TrackInfo>(); // Empty list
            }

            return result;
        }
    }
}
