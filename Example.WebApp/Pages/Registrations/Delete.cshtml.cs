#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example.WebApplication.Pages.Registrations
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SiteContext _context;

        public DeleteModel(SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Input = await _context.UserRegistrations.FindAsync(id);

            if (Input != null)
            {
                _context.UserRegistrations.Remove(Input);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
