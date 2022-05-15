namespace Example.WebApi.Endpoints;

public interface IPageable
{
    int PageIndex { get; set; }

    int PageSize { get; set; }
}
