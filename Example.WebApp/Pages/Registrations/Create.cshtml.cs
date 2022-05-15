#nullable disable
using Example.WebApplication.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Example.WebApplication.Pages.Registrations
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SiteContext _context;
        private readonly ICountryReader _countryReader;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public UserRegistration Input { get; set; } = default!;

        public IEnumerable<SelectListItem> Countries => _countryReader.GetCountries();

        public CreateModel(SiteContext context, ICountryReader countryReader, ILogger<CreateModel> logger)
        {
            _context = context;
            _countryReader = countryReader;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Registering user {Name}", Input.Name);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserRegistrations.Add(Input);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
