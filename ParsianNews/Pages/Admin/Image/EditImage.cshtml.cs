using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using System.Net;

namespace ParsianNews.Pages.Admin.Image
{
    public class EditImageModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public EditImageModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Models.Image Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, string? returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = (await _context.Images.Include(i => i.Gallery).FirstOrDefaultAsync(m => m.ImageId == id))!;
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "GalleryName");
            ViewData["ReturnUrl"] = returnUrl;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(IFormFile? imageUp, int? id, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "GalleryName");
                return Page();
            }

            var galleryName = await _context.Images
                .Include(i => i.Gallery)
                .Where(w => w.ImageId == id)
                .Select(s => s.Gallery!.GalleryName)
                .FirstOrDefaultAsync();

            var newGallery = await _context.Galleries
                .Where(w => w.GalleryId == Image.GalleryId)
                .Select(s => s.GalleryName)
                .FirstOrDefaultAsync();

            if (galleryName == null)
            {
                // Handle the case where galleryName is null
                ModelState.AddModelError(string.Empty, "Gallery not found.");
                return Page();
            }

            if (imageUp != null)
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Img/GalleryImages/{galleryName}/", Image.ImageName!);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

                string saveDir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Img/GalleryImages/{galleryName}/");
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);

                Image.ImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUp.FileName);
                string savePath = Path.Combine(saveDir, Image.ImageName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                    await imageUp.CopyToAsync(fileStream);
            }

            if (galleryName != newGallery)
            {
                string previousSaveDir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Img/GalleryImages/{galleryName}/");
                string previousSavePath = Path.Combine(previousSaveDir, Image.ImageName!);

                string newSaveDir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Img/GalleryImages/{newGallery}/");
                if (!Directory.Exists(newSaveDir))
                    Directory.CreateDirectory(newSaveDir);

                string newSavePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Img/GalleryImages/{newGallery}/", Image.ImageName!);
                System.IO.File.Move(previousSavePath, newSavePath, true);
            }

            _context.Attach(Image).State = EntityState.Modified;
            await _context.SaveChangesAsync(); // Don't forget to save the changes
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var encodedGalleryName = WebUtility.UrlEncode(returnUrl);
                return LocalRedirect($"~/Admin/Image?galleryName={encodedGalleryName}&handler=Gallery");
            }

            return RedirectToPage("./Index");
        }

    }
}

