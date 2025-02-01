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

        public async Task<ActionResult> OnGetAsync(string group, int pageId = 1)
        {
            int take = 2;
            int skip = (pageId - 1) * take;

            int count = _context.Reports
                .Include(i => i.ReportGroup)
                .Count(c => c.ReportGroup!.GroupName == group);

            ViewData["Count"] = count;
            ViewData["Take"] = take;
            ViewData["PageId"] = pageId;
            ViewData["PreviousPage"] = pageId - 1;
            ViewData["NextPage"] = pageId + 1;

            if (count % 2 == 0)
            {
                ViewData["PageCount"] = (count / take);
            }
            else
            {
                ViewData["PageCount"] = (count / take) + 1;
            }

            Reports = await _context.Reports
                .Include(i => i.ReportGroup)
                .Where(w => w.ReportGroup!.GroupName == group)
                .OrderByDescending(o => o.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            ViewData["GroupName"] = group;

            return Page();
        }
    }
}
