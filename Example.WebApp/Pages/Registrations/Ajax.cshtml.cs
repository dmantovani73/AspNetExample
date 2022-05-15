using Example.WebApplication.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Example.WebApplication.Pages.Registrations
{
    public class AjaxModel : PageModel
    {
        private readonly ICountryReader _countryReader;
        private readonly IConfiguration _configuration;

        public IEnumerable<SelectListItem> Countries => _countryReader.GetCountries(includeEmpty: true);

        public AjaxModel(ICountryReader countryReader, IConfiguration configuration)
        {
            _countryReader = countryReader;
            _configuration = configuration;
        }

        public void OnGet()
        {
            ViewData["ApiUrl"] = _configuration["ApiUrl"];
        }
    }
}
