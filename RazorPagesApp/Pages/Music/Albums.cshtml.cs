using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlaylistManager.Models;
using RazorPagesApp.Extensions;

namespace RazorPagesApp.Pages.Music
{
    public class AlbumsModel : PageModel
    {
        [BindProperty]
        public IList<AlbumInfo> Albums { get; set; }

        private HttpClient client = new HttpClient();

        public async Task OnGetAsync()
        {
            var data = await client.GetAsync(Request.Scheme + "://" + Request.Host.Value + "/api/Album/All?pageNum=1&pageSize=20");
            Albums = data.ContentAsType<List<AlbumInfo>>();
        }
    }
}