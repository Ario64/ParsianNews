using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ParsianNews.Pages.Admin.ReportGroup
{
    public class GroupDetailsModel : PageModel
    {
        private readonly ParsianNews.Data.ParsianNewsDbContext _context;

        public GroupDetailsModel(ParsianNews.Data.ParsianNewsDbContext context)
        {
            _context = context;
        }

        public Models.ReportGroup? ReportGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportGroup = await _context.ReportGroups.FirstOrDefaultAsync(m => m.GroupId == id);
            if (ReportGroup == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
