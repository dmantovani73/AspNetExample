namespace Example.WebApi.Endpoints.Users.Update;

public class Endpoint : Endpoint<UserRegistration, UserRegistration>
{
    public SiteContext Context { get; set; } = default!;

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes(RouteName.Users);
    }

    public override async Task HandleAsync(UserRegistration req, CancellationToken ct)
    {
        var user = await Context.UserRegistrations.FindAsync(req.Id);
        if (user == null) ThrowError("User not found");

        user!.Name = req.Name;
        user.Email = req.Email;
        user.PhoneNumber = req.PhoneNumber;
        user.BirthDate = req.BirthDate;
        user.Country = req.Country;
        await Context.SaveChangesAsync();

        Response = user;
    }
}
