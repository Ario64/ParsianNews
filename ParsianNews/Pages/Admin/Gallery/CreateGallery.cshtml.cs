using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Gallery
{
    public class CreateGalleryModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public CreateGalleryModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Gallery Gallery { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Galleries.Add(Gallery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
