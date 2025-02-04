using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Gallery
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IList<Models.Gallery> Gallery { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Gallery = await _context.Galleries.ToListAsync();
        }
    }
}
