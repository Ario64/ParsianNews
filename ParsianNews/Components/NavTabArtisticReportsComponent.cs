using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ReportGroup = ParsianNews.Utilities.ReportGroup;


namespace ParsianNews.Components;

public class NavTabArtisticReportsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public NavTabArtisticReportsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Report>? Reports { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = ReportGroup.Artistic, int take = 3)
    {
        Reports = await _context.Reports
            .Where(w => w.ReportGroup!.GroupName == groupName)
            .OrderByDescending(o => o.CreateDate)
            .Take(take)
            .ToListAsync();

        return View("/Pages/Components/_NavTabArtisticReports.cshtml", Reports);
    }
}