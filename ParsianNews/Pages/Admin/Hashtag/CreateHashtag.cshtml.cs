using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Hashtag
{
    public class CreateHashtagModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public CreateHashtagModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Hashtag Hashtag { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Hashtags.Add(Hashtag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
