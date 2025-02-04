using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Gallery
{
    public class GalleryDetailsModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public GalleryDetailsModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Models.Gallery Gallery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gallery = (await _context.Galleries.FirstOrDefaultAsync(m => m.GalleryId == id))!;
            if (Gallery == null)
            {
                return NotFound();
            }
         
            return Page();
        }
    }
}
