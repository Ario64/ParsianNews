using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Pages
{
    public class AllReportsModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public AllReportsModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Report>? Reports { get; set; }

        public async Task<ActionResult> OnGetAsync(string group)
        {
            Reports = await _context.Reports
                .Include(i => i.ReportGroup)
                .Where(w => w.ReportGroup!.GroupName == group)
                .OrderByDescending(o => o.CreateDate)
                .ToListAsync();

            ViewData["GroupName"] = group;

            return Page();
        }
    }
}
