using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ParsianNews.Pages.Admin.Report
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ParsianNewsDbContext _context;

        public DetailsModel(Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Models.Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = (await _context.Reports.Include(i=>i.ReportGroup).FirstOrDefaultAsync(m => m.ReportId == id))!;
            if (Report == null) 
                return NotFound();
            
            return Page();
        }
    }
}
