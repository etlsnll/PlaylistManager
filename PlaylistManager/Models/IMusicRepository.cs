﻿using System.Collections.Generic;

namespace PlaylistManager.Models
{
    public interface IMusicRepository
    {
        IEnumerable<TrackInfo> GetAllTracks(int pageNum, int pageSize);

        IEnumerable<AlbumInfo> GetAllAlbums(int pageNum, int pageSize);

        int AllAlbumsCount { get; }

        int AllTracksCount { get; }

        int AddPlayList(string title);

        int AllPlaylistsCount { get; }

        IEnumerable<PlaylistSummary> GetAllPlaylists(int pageNum, int pageSize);

        PlaylistDetails GetPlaylist(int id);

        IEnumerable<TrackInfo> SearchTracks(string title, string artist, string album, int maxTake);

        PlaylistDetails UpdatePlayListTitle(int id, string title);

        TrackInfo PlayListAddTrack(int playlistId, int trackId, int trackNumInPlaylist);

        IEnumerable<TrackInfo> PlayListDeleteTrack(int id, int trackId);

        bool DeletePlayList(int id);

        IEnumerable<TrackInfo> PlayListMoveTrack(int id, int trackId, bool moveUp);
    }
}
