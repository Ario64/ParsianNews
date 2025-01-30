using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Pages
{
    public class ReportModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public ReportModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Report Report { get; set; }
        public IEnumerable<Report>? Reports { get; set; }

        public void OnGet(int id)
        {
            Report = _context.Reports.Include(i => i.ReportGroup).FirstOrDefault(f => f.ReportId == id)!;
            Report.ReportView += 1;
            _context.Reports.Update(Report);
            _context.SaveChanges();
            int take = 10;
            string groupName = Report.ReportGroup!.GroupName!;
            Reports = _context.Reports
                .Where(w => w.ReportGroup!.GroupName == groupName && w.ReportId != id)
                .OrderByDescending(o => o.ReportView)
                .Take(take)
                .ToList();
        }
    }
}
