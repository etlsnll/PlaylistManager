using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public interface IMusicRepository
    {
        IEnumerable<TrackInfo> GetAllTracks(int pageNum, int pageSize);
        int CountAllTracks();
    }
}
