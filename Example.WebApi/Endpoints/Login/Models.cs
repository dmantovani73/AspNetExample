namespace Example.WebApi.Endpoints.Login;

public class Request
{
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}

public class Response
{
    public string UserName { get; set; } = default!;

    public string Token { get; set; } = default!;

    public DateTime ExpireAt { get; set; }
}
