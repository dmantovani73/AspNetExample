namespace Example.WebApi.Endpoints.Users.Delete;

public class Endpoint : Endpoint<UserRegistration>
{
    public SiteContext Context { get; set; } = default!;

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes(RouteName.Users);
    }

    public override async Task HandleAsync(UserRegistration req, CancellationToken ct)
    {
        var user = await Context.UserRegistrations.FindAsync(req.Id);
        if (user == null) ThrowError("User not found");

        Context.UserRegistrations.Remove(user!);
        await Context.SaveChangesAsync();
    }
}
