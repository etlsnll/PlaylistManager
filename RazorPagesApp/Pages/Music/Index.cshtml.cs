using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlaylistManager.Models;

namespace RazorPagesApp.Pages.Music
{
    public class IndexModel : PageModel
    {
        private readonly PlaylistManager.Models.MusicLibraryContext _context;

        public IndexModel(PlaylistManager.Models.MusicLibraryContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get;set; }

        public async Task OnGetAsync()
        {
            Album = await _context.Albums.ToListAsync();
        }
    }
}
