using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ParsianNews.Pages.Admin.ReportGroup
{
    public class DeleteGroupModel : PageModel
    {
        private readonly ParsianNews.Data.ParsianNewsDbContext _context;

        public DeleteGroupModel(ParsianNews.Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.ReportGroup? ReportGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportGroup = (await _context.ReportGroups.FirstOrDefaultAsync(m => m.GroupId == id))!;

            if (ReportGroup == null)
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
            ReportGroup = (await _context.ReportGroups.FindAsync(id))!;
            if (ReportGroup != null)
            {
                if (ReportGroup.GroupImage != null)
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GroupImages", ReportGroup.GroupImage!);
                    if (System.IO.File.Exists(deleteImagePath))
                    {
                        System.IO.File.Delete(deleteImagePath);
                    }
                }

                _context.ReportGroups.Remove(ReportGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
