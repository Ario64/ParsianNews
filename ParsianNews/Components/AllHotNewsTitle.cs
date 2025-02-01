using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Components;

public class AllHotNewsTitle : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public AllHotNewsTitle(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public List<Report>? HotReportTitles { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(int skip = 1, int take = 9)
    {
        HotReportTitles = (await _context.Reports
            .Include(i=>i.ReportGroup)
            .Where(w => w.IsHotNews == true)
            .OrderByDescending(o => o.HotNewsDate)
            .Skip(skip)
            .Take(take)
            .ToListAsync())!;

        return View("/Pages/Components/_AllHotNewsTitle.cshtml", HotReportTitles);
    }
}