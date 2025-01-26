using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ParsianNews.Utilities;
using ReportGroup = ParsianNews.Utilities.ReportGroup;


namespace ParsianNews.Components;

public class NavTabSocialReportsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public NavTabSocialReportsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Report>? Reports { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = ReportGroup.Social, int take = 3)
    {
        Reports = await _context.Reports
            .Where(w => w.ReportGroup!.GroupName == groupName)
            .OrderByDescending(o => o.CreateDate)
            .Take(take)
            .ToListAsync();

        return View("/Pages/Components/_NavTabSocialReports.cshtml", Reports);
    }
}