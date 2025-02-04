using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class ImageDetailsModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public ImageDetailsModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Models.Image Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = (await _context.Images.FirstOrDefaultAsync(m => m.ImageId == id))!;
            if (Image == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
