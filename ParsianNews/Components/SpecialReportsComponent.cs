using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ReportGroup = ParsianNews.Utilities.ReportGroup;

namespace ParsianNews.Components;

public class SpecialReportsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public SpecialReportsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public List<Report>? Reports { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = ReportGroup.SpecialReport, int take= 4)
    {
        Reports = await _context.Reports
            .Where(w => w.ReportGroup!.GroupName == groupName)
            .OrderByDescending(o => o.CreateDate)
            .Skip(1)
            .Take(take)
            .ToListAsync();

        return View("/Pages/Components/_SpecialReportsComponent.cshtml", Reports);
    }

}