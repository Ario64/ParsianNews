using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ParsianNews.Pages.Admin.Report
{
    public class DeleteReportModel : PageModel
    {
        private readonly Data.ParsianNewsDbContext _context;

        public DeleteReportModel(Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = (await _context.Reports.Include(i=>i.ReportGroup).FirstOrDefaultAsync(m => m.ReportId == id))!;

            if (Report == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = (await _context.Reports.FindAsync(id))!;
            if (Report != null)
            {
                if (Report.ReportImage != null)
                {
                    string deleteImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ReportImages/", Report.ReportImage!);
                    if (System.IO.File.Exists(deleteImgPath))
                    {
                        System.IO.File.Delete(deleteImgPath);
                    }
                }

                _context.Reports.Remove(Report);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
