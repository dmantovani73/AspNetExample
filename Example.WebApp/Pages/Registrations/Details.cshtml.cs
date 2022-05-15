#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example.WebApplication.Pages.Registrations
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly SiteContext _context;

        public DetailsModel(SiteContext context)
        {
            _context = context;
        }

        public UserRegistration Input { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Input = await _context.UserRegistrations.FirstOrDefaultAsync(m => m.Id == id);

            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
