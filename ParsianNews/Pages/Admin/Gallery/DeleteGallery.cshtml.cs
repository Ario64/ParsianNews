using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Gallery
{
    public class DeleteGalleryModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public DeleteGalleryModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                Gallery = gallery;
                _context.Galleries.Remove(Gallery);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
