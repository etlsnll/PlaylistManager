using System;
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
            var num = _musicRepository.CountAllTracks();
            //_logger.LogInformation("Count All tracks method called - counted {0} tracks", num);
            return num;
        }
        
        // GET: api/MusicLibrary/AllTracks
        [HttpGet("[action]")]
        public IEnumerable<TrackInfo> AllTracks(int pageNum, int pageSize)
        {
            return _musicRepository.GetAllTracks(pageNum, pageSize);
        }






        // GET: api/MusicLibrary/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/MusicLibrary
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/MusicLibrary/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
