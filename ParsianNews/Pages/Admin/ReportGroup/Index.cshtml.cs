using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.ReportGroup
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public List<Models.ReportGroup>? ReportGroups { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ReportGroups = await _context.ReportGroups.ToListAsync();
            return Page();
        }
    }
}
