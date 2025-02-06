using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Image
{
    public class EditImageModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public EditImageModel(ParsianNewsDbContext context)
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

            Image = (await _context.Images.FirstOrDefaultAsync(m => m.ImageId == id))!;
            if (Image == null)
            {
                return NotFound();
            }
            ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imageUp, int? galleryId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GalleryId"] = new SelectList(_context.Galleries, "GalleryId", "Description");
                return Page();
            }

            if (imageUp != null)
            {
                var galleryName = _context.Galleries.Where(w => w.GalleryId == galleryId).Select(s => s.GalleryName)
                    .FirstOrDefaultAsync();
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/GalleryImage/{galleryName}/", Image.ImageName!);
                if (Directory.Exists(imgPath))
                {
                    Directory.Delete(imgPath);
                }

                Image.ImageName = Guid.NewGuid() + Path.GetExtension(imageUp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/GalleryImage/{galleryName}/", Image.ImageName!);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                     await imageUp.CopyToAsync(stream);
                }
            }
            _context.Attach(Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(Image.ImageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
