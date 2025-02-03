using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Hashtag
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IList<Models.Hashtag> Hashtag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Hashtag = await _context.Hashtags.ToListAsync();
        }
    }
}
