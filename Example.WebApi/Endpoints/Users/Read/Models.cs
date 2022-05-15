namespace Example.WebApi.Endpoints.Users.Read;

public class Request : IPageable, ISortable
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BirthDate { get; set; }

    public string? Country { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string? SortField { get; set; }

    public string? SortOrder { get; set; }
}

public class Response
{
    static readonly IList<UserRegistration> Empty = new List<UserRegistration>();

    public IList<UserRegistration> Data { get; set; } = Empty;

    public int ItemsCount { get; set; }
}

