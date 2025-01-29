using Microsoft.AspNetCore.Mvc;
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

        public void OnGet(int id)
        {
            Report = _context.Reports.Include(i=>i.ReportGroup).FirstOrDefault(f => f.ReportId == id)!;
        }
    }
}
