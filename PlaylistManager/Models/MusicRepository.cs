using System;
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
    }
}
