using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class DeleteImageModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public DeleteImageModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Image Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = (await _context.Images.Include(i => i.Gallery).FirstOrDefaultAsync(m => m.ImageId == id))!;

            if (Image == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Image == null)
            {
                return NotFound();
            }
            _context.Images.Remove(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
