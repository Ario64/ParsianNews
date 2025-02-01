using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;
using ReportGroup = ParsianNews.Utilities.ReportGroup;

namespace ParsianNews.Components;

public class FirstHotNewsComponent : ViewComponent
{
    private readonly ParsianNewsDbContext _context;

    public FirstHotNewsComponent(ParsianNewsDbContext context)
    {
        _context = context;
    }

    public Report? Report { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Report = await _context.Reports
            .Where(w=>w.IsHotNews == true)
            .OrderByDescending(o => o.HotNewsDate)
            .FirstOrDefaultAsync();

        return View("/Pages/Components/_FirstHotNews.cshtml", Report);
    }
}