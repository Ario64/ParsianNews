using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IList<Models.Image> Image { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Image = await _context.Images
                .Include(i => i.Gallery).ToListAsync();
        }
    }
}
