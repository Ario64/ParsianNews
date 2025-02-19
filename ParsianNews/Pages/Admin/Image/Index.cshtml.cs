using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using System.Net;

namespace ParsianNews.Pages.Admin.Image
{
    public class IndexModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public IndexModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IList<Models.Image> Images { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? returnUrl)
        {
            ViewData["Galleries"] = await _context.Galleries.Select(s => s.GalleryName).ToListAsync();
            Images = await _context.Images.Include(i => i.Gallery).ToListAsync();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                var encodedGalleryName = WebUtility.UrlEncode(returnUrl);
                return LocalRedirect($"~/Admin/Image?galleryName={encodedGalleryName}&handler=Gallery");
            }
            return Page();
        }

        public async Task OnGetGallery(string galleryName)
        {
            ViewData["CurrentGallery"] = galleryName;

            Images = await _context.Images
                .Include(i => i.Gallery)
                .Where(w => w.Gallery!.GalleryName == galleryName)
                .OrderByDescending(o => o.CreateDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnGetDelete(int id, string? returnUrl)
        {

            if (id <= 0)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Where(w => w.ImageId == id)
                .FirstOrDefaultAsync();

            var galleryName = await _context.Images
                .Where(w => w.ImageId == id)
                .Select(s => s.Gallery!.GalleryName)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(image!.ImageName))
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot/Img/GalleryImages/{galleryName}/", image.ImageName);

                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                var encodedGalleryName = WebUtility.UrlEncode(returnUrl);
                return LocalRedirect($"~/Admin/Image?galleryName={encodedGalleryName}&handler=Gallery");
            }
            return Page();
        }
    }
}
