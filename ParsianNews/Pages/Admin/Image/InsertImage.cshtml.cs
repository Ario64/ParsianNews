using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Images.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
