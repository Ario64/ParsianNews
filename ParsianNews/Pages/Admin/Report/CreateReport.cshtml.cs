using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParsianNews.Validators;

namespace ParsianNews.Pages.Admin.Report
{
    public class CreateReportModel : PageModel
    {
        private readonly Data.ParsianNewsDbContext _context;

        public CreateReportModel(Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ReportGroup();
            return Page();
        }

        [BindProperty]
        public Models.Report Report { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imgUp)
        {
            if (!ModelState.IsValid)
            {
                ReportGroup();
                return Page();
            }

            Report.CreateDate = DateTime.Now;
            Report.ReportView = 0;
            if (imgUp != null && imgUp.IsImage())
            {
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ReportImages/");
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                Report.ReportImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUp.FileName);
                string saveImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ReportImages/", Report.ReportImage);
                using (var stream = new FileStream(saveImgPath, FileMode.Create))
                {
                    await imgUp.CopyToAsync(stream);
                }
            }
            else
            {
                Report.ReportImage = "Default.jpg";
            }

            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        void ReportGroup()
        {
            ViewData["ReportGroupId"] = new SelectList(_context.ReportGroups, "GroupId", "GroupName");
        }
    }
}
