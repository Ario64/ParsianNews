using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Components;

public class HealthReportsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public HealthReportsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Report>? Reports { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = "سلامت", int take = 4)
    {
        Reports = await _context.Reports
            .Where(w => w.ReportGroup!.GroupName == groupName)
            .OrderByDescending(o => o.CreateDate)
            .Take(take)
            .ToListAsync();

        return View("/Pages/Components/_HealthReports.cshtml", Reports);
    }
}