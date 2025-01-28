using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Validators;

namespace ParsianNews.Pages.Admin.Report
{
    public class EditReportModel : PageModel
    {
        private readonly Data.ParsianNewsDbContext _context;

        public EditReportModel(Data.ParsianNewsDbContext context)
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

            Report = (await _context.Reports.Include(i => i.ReportGroup).FirstOrDefaultAsync(m => m.ReportId == id))!;
            if (Report == null)
            {
                return NotFound();
            }

            ViewData["ReportGroupId"] = new SelectList(_context.ReportGroups, "GroupId", "GroupName");
            return Page();
        }

        // To protect from over posting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imgUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (imgUp != null)
            {
                string deleteImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ReportImages/", Report.ReportImage!);
                if (System.IO.File.Exists(deleteImgPath))
                {
                    System.IO.File.Delete(deleteImgPath);
                }

                Report.ReportImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUp.FileName);
                string saveImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ReportImages/", Report.ReportImage);
                await using (var stream = new FileStream(saveImgPath, FileMode.Create))
                {
                    await imgUp.CopyToAsync(stream);
                }
            }

            _context.Attach(Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.ReportId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
