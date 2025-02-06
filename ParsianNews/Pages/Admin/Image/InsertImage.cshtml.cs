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
        public async Task<IActionResult> OnPostAsync(List<IFormFile>? files, int galleryId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "GalleryName");
                return Page();
            }

            if (files != null)
            {
                var galleryName = await _context.Galleries
                    .Where(w => w.GalleryId == galleryId)
                    .Select(s => s.GalleryName)
                    .FirstOrDefaultAsync();

                string imgPath = "wwwroot/ImageGallery/" + galleryName + "/";
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string imgName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), imgPath, imgName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        Image = new Models.Image()
                        {
                            ImageName = imgName,
                            CreateDate = DateTime.Now,
                            GalleryId = galleryId,
                        };
                    }
                    _context.Images.Add(Image);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
