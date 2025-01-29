using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ReportGroup = ParsianNews.Utilities.ReportGroup;

namespace ParsianNews.Components;

public class FirstSpecialReportComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public FirstSpecialReportComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public Report? Report { get; set; }

    public async Task<IViewComponentResult> InvokeAsync(string groupName = ReportGroup.SpecialReport)
    {
        Report = await _context.Reports
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefaultAsync(w => w.ReportGroup!.GroupName == groupName);

        return View("/Pages/Components/_FirstSpecialReport.cshtml", Report);
    }
}