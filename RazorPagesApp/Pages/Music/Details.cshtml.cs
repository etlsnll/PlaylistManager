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
    public class DetailsModel : PageModel
    {
        private readonly PlaylistManager.Models.MusicLibraryContext _context;

        public DetailsModel(PlaylistManager.Models.MusicLibraryContext context)
        {
            _context = context;
        }

        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumId == id);

            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
