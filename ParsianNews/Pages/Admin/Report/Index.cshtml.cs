using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ParsianNews.Pages.Admin.Report
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNews.Data.ParsianNewsDbContext _context;

        public IndexModel(ParsianNews.Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Models.Report> Report { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Report = await _context.Reports
                .Include(r => r.ReportGroup)
                .OrderByDescending(o=>o.CreateDate)
                .ToListAsync();
        }
    }
}
