using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IList<Models.Image> Image { get; set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            ViewData["Galleries"] = await _context.Galleries.Select(s => s.GalleryName).ToListAsync();

            Image = await _context.Images.Include(i => i.Gallery).ToListAsync();
        }

        public async Task OnGetGallery(string galleryName)
        {
            ViewData["CurrentGallery"] = galleryName;

            Image = await _context.Images
                .Include(i => i.Gallery)
                .Where(w => w.Gallery!.GalleryName == galleryName)
                .OrderByDescending(o=>o.CreateDate)
                .ToListAsync();
        }
    }
}
