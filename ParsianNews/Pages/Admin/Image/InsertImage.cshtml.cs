using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class InsertImageModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public InsertImageModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "GalleryName");
            return Page();
        }

        [BindProperty]
        public Models.Image Image { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imageUp, int galleryId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "GalleryName");
                return Page();
            }

            if (imageUp != null)
            {
                var galleryName = await _context.Galleries
                    .Where(w => w.GalleryId == galleryId)
                    .Select(s => s.GalleryName)
                    .FirstOrDefaultAsync();

                string imgPath = $"wwwroot/ImageGallery/{galleryName}/";
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                string imgName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUp.FileName);
                string saveImgPath = Path.Combine(Directory.GetCurrentDirectory(),imgPath ,imgName);
                using (var stream = new FileStream(saveImgPath, FileMode.Create))
                {
                    await imageUp.CopyToAsync(stream);
                }

                Image = new Models.Image()
                {
                    ImageName = imgName,
                    CreateDate = DateTime.Now,
                    GalleryId = galleryId,
                };
            }


            _context.Images.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
