using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlaylistManager.Models;

namespace RazorPagesApp.Pages.Music
{
    public class CreateModel : PageModel
    {
        private readonly PlaylistManager.Models.MusicLibraryContext _context;

        public CreateModel(PlaylistManager.Models.MusicLibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Albums.Add(Album);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}