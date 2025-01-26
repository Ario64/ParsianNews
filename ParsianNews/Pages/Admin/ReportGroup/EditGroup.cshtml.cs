using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Validators;

namespace ParsianNews.Pages.Admin.ReportGroup
{
    public class EditGroupModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public EditGroupModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Models.ReportGroup? ReportGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReportGroup = (await _context.ReportGroups.FirstOrDefaultAsync(f => f.GroupId == id))!;
            if (ReportGroup == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? imgUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (imgUp != null && imgUp.IsImage())
            {
                string deleteImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GroupImages/" + ReportGroup!.GroupImage);
                if (System.IO.File.Exists(deleteImgPath))
                {
                    System.IO.File.Delete(deleteImgPath);
                }

                ReportGroup.GroupImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUp.FileName);
                string saveImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GroupImages/", ReportGroup.GroupImage);

                await using (var stream = new FileStream(saveImgPath, FileMode.Create))
                {
                    await imgUp.CopyToAsync(stream);
                }
            }
            _context.ReportGroups.Update(ReportGroup!);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

