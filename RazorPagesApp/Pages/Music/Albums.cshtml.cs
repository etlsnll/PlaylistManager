using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlaylistManager.Models;
using RazorPagesApp.Extensions;

namespace RazorPagesApp.Pages.Music
{
    public class AlbumsModel : PageModel
    {
        [BindProperty]
        public IList<AlbumInfo> Albums { get; set; }

        [BindProperty]
        public int TotalAlbums { get; set; }

        [BindProperty]
        public int PageNum { get; set; }

        [BindProperty]
        public int PageSize { get; set; }

        [BindProperty]
        public string SelectedPageSizeVal { get; set; } // Need this to set selected in page size drop down

        public List<SelectListItem> pageSizes = new List<SelectListItem>();

        private HttpClient client = new HttpClient();

        public async Task OnGetAsync(int? pageNum, int? pageSize)
        {
            PageNum = pageNum ?? 1;
            PageSize = pageSize ?? 10;
            for (int i = 10; i < 60; i += 10)
                pageSizes.Add(new SelectListItem() { Value = String.Format($"{Request.Scheme}://{Request.Host.Value}/Music/Albums/?pageNum={PageNum}&pageSize={i}",
                                                                           Request.Scheme, Request.Host.Value, PageNum, i),
                                                     Text = i.ToString()});
            SelectedPageSizeVal = pageSizes.First(s => s.Text == PageSize.ToString()).Value;

            var data = await client.GetAsync(String.Format($"{Request.Scheme}://{Request.Host.Value}/api/Album/All/page/{PageNum}/size/{PageSize}", 
                                                           Request.Scheme, Request.Host.Value, PageNum, PageSize));
            var result = data.ContentAsType<Tuple<int,List<AlbumInfo>>>();
            TotalAlbums = result.Item1;
            Albums = result.Item2;
        }
    }
}