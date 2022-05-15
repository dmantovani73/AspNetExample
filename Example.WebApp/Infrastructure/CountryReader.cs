using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Example.WebApplication.Infrastructure;

public interface ICountryReader
{
    IEnumerable<SelectListItem> GetCountries(bool includeEmpty = false);
}

public class CountryReader : ICountryReader
{
    static readonly IEnumerable<SelectListItem> Empty = new List<SelectListItem>() { new SelectListItem() };

    public IEnumerable<SelectListItem> GetCountries(bool includeEmpty = false)
    {
        var result =
            (
                from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                let ri = new RegionInfo(ci.LCID)
                let code = ri.Name
                let name = ri.EnglishName
                orderby name
                select new SelectListItem(name, code)
            ).DistinctBy(q => q.Value);

        return includeEmpty
            ? Empty.Union(result)
            : result;
    }
}
