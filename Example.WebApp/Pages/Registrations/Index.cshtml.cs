#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example.WebApplication.Pages.Registrations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SiteContext _context;

        public IndexModel(SiteContext context)
        {
            _context = context;
        }

        public IList<UserRegistration> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.UserRegistrations.ToListAsync();
        }
    }
}
