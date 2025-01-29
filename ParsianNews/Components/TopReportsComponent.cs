using System.CodeDom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ReportGroup = ParsianNews.Utilities.ReportGroup;

namespace ParsianNews.Components;

public class TopReportsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public TopReportsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public List<Report>? Reports { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = ReportGroup.MultiMedia, int take = 4)
    {
        Reports =await _context.Reports
            .Where(w => w.ReportGroup!.GroupName != groupName)
            .OrderByDescending(q => q.ReportView)
            .Take(take)
            .ToListAsync();
        return View("/Pages/Components/_TopReports.cshtml", Reports);
    }
}