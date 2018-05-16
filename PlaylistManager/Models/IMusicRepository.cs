using System.Collections.Generic;

namespace PlaylistManager.Models
{
    public interface IMusicRepository
    {
        IEnumerable<TrackInfo> GetAllTracks(int pageNum, int pageSize);

        int CountAllTracks();

        void AddPlayList(string title);
    }
}
