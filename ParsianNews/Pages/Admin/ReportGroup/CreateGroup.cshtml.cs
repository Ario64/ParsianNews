using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParsianNews.Validators;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.ReportGroup
{
    public class CreateGroupModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public CreateGroupModel(ParsianNewsDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Models.ReportGroup? ReportGroup { get; set; }

        public IActionResult OnGet()
        {
            ReportGroup = new Models.ReportGroup();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imgUp)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.ReportGroups.Any(a => a.GroupName == ReportGroup!.GroupName))
            {
                ModelState.AddModelError("", errorMessage:"نام گروه وارد شده از قبل وجود دارد!");
                return Page();
            }
            
            if (imgUp != null && imgUp.IsImage())
            {
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GroupImages/");
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                ReportGroup!.GroupImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUp.FileName);
                string saveImgPath = imgPath + ReportGroup.GroupImage;

                await using (var stream = new FileStream(saveImgPath, FileMode.Create))
                {
                    await imgUp.CopyToAsync(stream);
                }
            }
            else
            {
                ReportGroup!.GroupImage = "Default.jpg";
            }

            _context.ReportGroups.Add(ReportGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
