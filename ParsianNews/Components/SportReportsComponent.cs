using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Components
{
    public class SportReportsComponent : ViewComponent
    {
        private readonly ParsianNewsDbContext _context;

        public SportReportsComponent(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Report>? Reports { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string groupName = "ورزشی", int take = 4)
        {
            Reports =await _context.Reports
                .Where(w=>w.ReportGroup!.GroupName == groupName)
                .OrderByDescending(o=>o.CreateDate)
                .Take(take)
                .ToListAsync();

            return View("/Pages/Components/_SportReports.cshtml", Reports);
        }
    }
}
