namespace Example.WebApi.Endpoints.Users.Create;

public class Endpoint : Endpoint<UserRegistration, UserRegistration>
{
    public SiteContext Context { get; set; } = default!;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes(RouteName.Users);
    }

    public override async Task HandleAsync(UserRegistration req, CancellationToken ct)
    {
        Context.UserRegistrations.Add(req);
        await Context.SaveChangesAsync();

        Response = req;
    }
}
