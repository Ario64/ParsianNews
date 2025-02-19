using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using ParsianNews.Models;

namespace ParsianNews.Components
{
    public class HashtagComponent : ViewComponent
    {
        private readonly ParsianNewsDbContext _context;

        public HashtagComponent(ParsianNewsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Hashtag> Hashtags { get; set; } = null!;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Hashtags = await _context.Hashtags.ToListAsync();
            return View("/Pages/Components/HashtagComponent.cshtml", Hashtags);
        }
    }
}
