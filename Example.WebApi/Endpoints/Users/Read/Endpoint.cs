namespace Example.WebApi.Endpoints.Users.Read;

public class Endpoint : Endpoint<Request, Response>
{
    public SiteContext Context { get; set; } = default!;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes(RouteName.Users);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = Context.UserRegistrations as IQueryable<UserRegistration>;
        query = ApplyWhere(query, req);

        // Conto il numero totale di elementi prima di applicare il paging.
        var itemsCount = await query.CountAsync(ct);

        query = ApplyPaging(query, req);
        query = ApplySorting(query, req);

        var data = await query.ToListAsync(ct);

        Response = new Response
        {
            Data = data,
            ItemsCount = itemsCount,
        };
    }

    IQueryable<UserRegistration> ApplyWhere(IQueryable<UserRegistration> query, Request req)
    {
        if (!string.IsNullOrEmpty(req.Name)) query = query.Where(p => p.Name.Contains(req.Name));
        if (!string.IsNullOrEmpty(req.Email)) query = query.Where(p => p.Email.Contains(req.Email));
        if (!string.IsNullOrEmpty(req.PhoneNumber)) query = query.Where(p => p.PhoneNumber != null && p.PhoneNumber.Contains(req.PhoneNumber));
        if (!string.IsNullOrEmpty(req.BirthDate))
        {
            if (DateTime.TryParse(req.BirthDate, out var birthDate)) query = query.Where(p => p.BirthDate == birthDate);
        }

        if (!string.IsNullOrEmpty(req.Country)) query = query.Where(p => p.Country.Contains(req.Country));

        return query;
    }

    IQueryable<UserRegistration> ApplyPaging(IQueryable<UserRegistration> query, Request req)
    {
        return query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize);
    }

    IQueryable<UserRegistration> ApplySorting(IQueryable<UserRegistration> query, Request req)
    {
        if (req?.SortField == null) return query.OrderBy(p => p.Id);

        if (!Enum.TryParse<SortOrder>(req.SortOrder, true, out var sortOrder)) sortOrder = SortOrder.Asc;

        if (string.Compare(req.SortField, nameof(Common.Models.UserRegistration.Id), true) == 0)
            query = sortOrder == SortOrder.Asc ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);

        if (string.Compare(req.SortField, nameof(Common.Models.UserRegistration.Name), true) == 0)
            query = sortOrder == SortOrder.Asc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

        if (string.Compare(req.SortField, nameof(Common.Models.UserRegistration.Email), true) == 0)
            query = sortOrder == SortOrder.Asc ? query.OrderBy(p => p.Email) : query.OrderByDescending(p => p.Email);

        if (string.Compare(req.SortField, nameof(Common.Models.UserRegistration.PhoneNumber), true) == 0)
            query = sortOrder == SortOrder.Asc ? query.OrderBy(p => p.PhoneNumber) : query.OrderByDescending(p => p.PhoneNumber);

        if (string.Compare(req.SortField, nameof(Common.Models.UserRegistration.BirthDate), true) == 0)
            query = sortOrder == SortOrder.Asc ? query.OrderBy(p => p.BirthDate) : query.OrderByDescending(p => p.BirthDate);

        return query;
    }

    enum SortOrder
    {
        Asc,
        Desc,
    }
}
