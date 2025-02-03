using Microsoft.AspNetCore.Mvc.RazorPages;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Hashtag
{
    public class DetailsModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public DetailsModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Models.Hashtag Hashtag { get; set; } = default!;

        public void OnGet(int id)
        {
            Hashtag = _context.Hashtags.FirstOrDefault(f => f.HashtagId == id)!;
        }
    }
}
