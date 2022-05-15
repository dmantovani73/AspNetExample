using Microsoft.AspNetCore.Identity;

namespace Example.WebApi.Endpoints.Login;

public class Endpoint : Endpoint<Request, Response>
{
    public UserManager<IdentityUser> UserManager { get; set; } = default!;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await UserManager.FindByNameAsync(req.UserName);
        if (user == null)
        {
            ThrowError("User not found");
            return;
        }

        if (!await UserManager.CheckPasswordAsync(user, req.Password))
        {
            ThrowError("Wrong password");
            return;
        }

        var signingKey = Config!["TokenSigningKey"];
        var tokenExpiry = Config.GetValue("JwtTokenDuration", TimeSpan.FromMinutes(30));
        var expireAt = DateTime.UtcNow.Add(tokenExpiry);

        var jwtToken = JWTBearer.CreateToken(
            signingKey: signingKey,
            expireAt: expireAt,
            claims: new[] 
            { 
                ("UserName", req.UserName),
            });

        Response = new Response
        {
            UserName = req.UserName,
            Token = jwtToken,
            ExpireAt = expireAt,
        };
    }
}
