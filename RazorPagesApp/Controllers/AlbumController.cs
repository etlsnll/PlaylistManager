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

        /// <summary>
        /// GET: api/Album/All (server side paged results)
        /// </summary>
        /// <param name="pageNum">Page number to get</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of music track info classes</returns>
        [HttpGet("[action]")]
        public IEnumerable<AlbumInfo> All(int pageNum, int pageSize)
        {
            if (pageNum < 1)
                throw new ArgumentException("Value must be greater than 0", "pageNum");
            if (pageSize < 1)
                throw new ArgumentException("Value must be greater than 0", "pageSize");

            return _musicRepository.GetAllAlbums(pageNum, pageSize);
        }
    }
}