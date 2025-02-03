using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParsianNews.Data;

namespace ParsianNews.Pages.Admin.Hashtag
{
    public class EditHashtagModel : PageModel
    {
        private ParsianNewsDbContext _context;

        public EditHashtagModel(ParsianNewsDbContext context)
        {
            _context = context;
        }

        [BindProperty] 
        public Models.Hashtag Hashtag { get; set; }

        public void OnGet(int id)
        {
            Hashtag = _context.Hashtags.FirstOrDefault(f => f.HashtagId == id)!;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Hashtags.Update(Hashtag);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
