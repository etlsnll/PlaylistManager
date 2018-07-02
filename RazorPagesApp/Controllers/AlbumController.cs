using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlaylistManager.Models;

namespace RazorPagesApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Album")]
    public class AlbumController : Controller
    {
        private IMusicRepository _musicRepository;
        private readonly ILogger _logger;

        public AlbumController(IMusicRepository musicRepository,
                               ILogger<AlbumController> logger)
        {
            _musicRepository = musicRepository;
            _logger = logger;
        }

        // GET: api/Album/Count
        [HttpGet("[action]")]
        public int Count()
        {
            var num = _musicRepository.AllAlbumsCount;
            return num;
        }

        /// <summary>
        /// GET: api/Album/All (server side paged results)
        /// </summary>
        /// <param name="pageNum">Page number to get</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of music track info classes</returns>
        [HttpGet("[action]/page/{pageNum}/size/{pageSize}")]
        public Tuple<int, IEnumerable<AlbumInfo>> All(int pageNum, int pageSize)
        {
            if (pageNum < 1)
                throw new ArgumentException("Value must be greater than 0", "pageNum");
            if (pageSize < 1)
                throw new ArgumentException("Value must be greater than 0", "pageSize");

            var num = _musicRepository.AllAlbumsCount;
            var albums = _musicRepository.GetAllAlbums(pageNum, pageSize);

            return new Tuple<int, IEnumerable<AlbumInfo>>(num, albums);
        }
    }
}