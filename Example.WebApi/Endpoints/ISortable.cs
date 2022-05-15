namespace Example.WebApi.Endpoints;

public interface ISortable
{
    public string? SortField { get; set; }

    public string? SortOrder { get; set; }
}