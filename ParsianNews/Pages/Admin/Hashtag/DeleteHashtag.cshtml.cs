using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Hashtag
{
    public class DeleteHashtagModel : PageModel
    {
        private readonly ParsianNewsDbContext _context;

        public DeleteHashtagModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Hashtag Hashtag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hashtag = (await _context.Hashtags.FirstOrDefaultAsync(m => m.HashtagId == id))!;

            if (Hashtag == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            _context.Hashtags.Remove(Hashtag);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
